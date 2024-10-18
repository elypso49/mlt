import {Injectable} from '@angular/core';
import {forkJoin, map, mergeMap, Observable, of} from 'rxjs';
import {environment} from "../../environments/environment";
import {SynoTask} from "../core/models/SynoTask";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class SynologyService {

  private apiDownloadStation = 'api/DownloadStation';


  constructor(private http: HttpClient) {
  }

  getSynoTasks(): Observable<SynoTask[]> {
    return this.http.get<{ data: SynoTask[], errors: any[], isSuccess: boolean }>(`${environment.services.MltApiEndpoint}/${this.apiDownloadStation}`).pipe(
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

  // downloadItems(rssFeedResults: RssFeedResult[]): Observable<RssFeedResult[]> {
  //   const observables: Observable<RssFeedResult>[] = rssFeedResults.map(item => this.downloadItem(item).pipe(map(() => item)));
  //
  //   return forkJoin(observables);
  // }
  //
  // downloadItem(rssFeedResult: RssFeedResult): Observable<RssFeedResult> {
  //   return of(rssFeedResult).pipe(
  //     map(downloadedItem => {
  //       downloadedItem.state |= StateValue.Downloaded;
  //       return downloadedItem;
  //     })
  //   );
  // }
}
