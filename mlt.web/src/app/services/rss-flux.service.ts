import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {forkJoin, map, mergeMap, Observable} from 'rxjs';
import {RssFeed} from "../core/models/rssFeed";
import {RssFeedResult} from "../core/models/RssFeedResult";
import {environment} from "../../environments/environment";
import {StateValue} from "../core/enums/StateValue";

@Injectable({
  providedIn: 'root'
})
export class RssFluxService {
  private getRssFeedEndpoint = 'api/RssFeeds';
  private putRssFeedResultEndpoint = 'api/RssFeedResults';
  private putRssFeedEndpoint = 'api/RssFeeds';


  constructor(private http: HttpClient) {
  }

  getAllRssFlux(): Observable<RssFeed[]> {
    console.log(`MLTApi : ${environment.services.MltApiEndpoint}`);
    console.log(`url called : ${`${environment.services.MltApiEndpoint}/${this.getRssFeedEndpoint}`}`)

    return this.http.get<{ data: RssFeed[] }>(`${environment.services.MltApiEndpoint}/${this.getRssFeedEndpoint}`).pipe(
      mergeMap(response => {
        const rssFeeds = response.data; // Extract the 'data' array from the response

        // Create an array of observables for the results of each feed
        const detailRequests = rssFeeds.map(feed => {
          return this.http.get<{
            data: RssFeedResult[]
          }>(`${environment.services.MltApiEndpoint}/${this.getRssFeedEndpoint}/${feed.id}/results`).pipe(
            map(response => ({
              ...feed,
              results: response.data // Extract the 'data' array and assign it to 'results'
            }))
          );
        });
        // Combine all observables into one
        return forkJoin(detailRequests);
      })
    );
  }

  updateRssFeedResult(rssFeedResult: RssFeedResult): Observable<boolean> {
    const serializedFeeds = JSON.stringify(rssFeedResult);

    return this.http.put<{
      isSuccess: boolean
    }>(`${environment.services.MltApiEndpoint}/${this.putRssFeedResultEndpoint}/${rssFeedResult.id}`, serializedFeeds, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Accept': '*/*'
      }
    }).pipe(
      map(response => {
        return response.isSuccess;
      })
    );
  }

  updateRssFeed(rssFeed: RssFeed): Observable<boolean> {
    const serializedFeeds = JSON.stringify(rssFeed);

    return this.http.put<{
      isSuccess: boolean
    }>(`${environment.services.MltApiEndpoint}/${this.putRssFeedEndpoint}/${rssFeed.id}`, serializedFeeds, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        'Accept': '*/*'
      }
    }).pipe(
      map(response => {
        return response.isSuccess;
      })
    );
  }


  refreshRssFeed(rssFeed: RssFeed): Observable<boolean> {

    return this.http.post<{
      isSuccess: boolean
    }>(`${environment.services.MltApiEndpoint}/${this.putRssFeedResultEndpoint}/${rssFeed.id}/fetch`, {
      headers: {
        'Accept': '*/*'
      }
    }).pipe(
      map(response => {
        return response.isSuccess;
      })
    );
  }


  // getAllRssFlux(): Observable<RssFeed[]> {
  //   let rssFeed = this.http.get<RssFeed[]>(this.getRssFeedEndpoint);
  //
  //   rssFeed.subscribe(
  //     data => console.log(data),   // Log the response data when it arrives
  //     error => console.error(error)  // Log any error if the request fails
  //   );
  //
  //
  //
  //   return this.http.get<RssFeed[]>(this.getRssFeedEndpoint);
  // }


  // getAllRssFlux(): Observable<RssFeed[]> {
  //   this.rssFeeds = this.http.get<RssFeed[]>("http://localhost:5000/RssFeedResults");
  //   this.obsRssFeed$ = new Observable<RssFeed[]>(observer => {
  //     observer.next(this.rssFeeds)
  //     observer.complete()
  //   })
  //
  //
  //   return this.obsRssFeed$;
  //   // return this.http.get<any>(this.apiUrl);
  // }

}
