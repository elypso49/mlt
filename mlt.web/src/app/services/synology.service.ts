import {Injectable} from '@angular/core';
import {catchError, forkJoin, map, mergeMap, Observable, of, tap} from 'rxjs';
import {environment} from "../../environments/environment";
import {SynoTask} from "../core/models/SynoTask";
import {HttpClient} from "@angular/common/http";
import {SynoFolder} from "../core/models/SynoFolder";

@Injectable({
  providedIn: 'root'
})
export class SynologyService {

  private apiDownloadStation = 'api/DownloadStation';
  private apiFileStation = 'api/FileStation';


  constructor(private http: HttpClient) {
  }

  getSynoTasks(): Observable<SynoTask[]> {
    return this.http.get<{
      data: SynoTask[],
      errors: any[],
      isSuccess: boolean
    }>(`${environment.services.MltApiEndpoint}/${this.apiDownloadStation}`).pipe(
      map(response => {
        if (response.isSuccess) {
          return response.data.map(task => ({
            ...task,
            sizeDownloaded: task.sizeDownloaded
          }));
        }
        throw new Error('Failed to fetch SynoTasks');
      })
    );
  }


  cleanSynoTasks(): Observable<boolean> {
    return this.http.get<void>(`${environment.services.MltApiEndpoint}/${this.apiDownloadStation}/clean`).pipe(
      tap(() => {
        // No response body is expected, simply log success or handle side effect here if needed
      }),
      map(() => true), // Always return true regardless of the response
      catchError((error) => {
        // Handle error if needed, for example, return false if an error occurs
        console.error('Failed to clean Synology tasks:', error);
        return of(false);
      })
    );
  }


  getSynoFolders(): Observable<SynoFolder[]> {
    return this.http.get<{
      data: SynoFolder[],
      errors: any[],
      isSuccess: boolean
    }>(`${environment.services.MltApiEndpoint}/${this.apiFileStation}`).pipe(
      map(response => {
        if (response.isSuccess) {
          return response.data.map(task => ({
            ...task
          }));
        }
        throw new Error('Failed to fetch SynFolders');
      })
    );
  }
}
