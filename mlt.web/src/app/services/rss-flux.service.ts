import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {forkJoin, map, mergeMap, Observable} from 'rxjs';
import {RssFeed} from "../core/models/rssFeed";
import {RssFeedResult} from "../core/models/RssFeedResult";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class RssFluxService {
  private endpoint = 'RssFeeds';

  constructor(private http: HttpClient) {
  }

  getAllRssFlux(): Observable<RssFeed[]> {
    console.log(`MLTApi : ${environment.services.MltApiEndpoint}`);
    console.log(`url called : ${`${environment.services.MltApiEndpoint}/${this.endpoint}`}`)

    return this.http.get<RssFeed[]>(`${environment.services.MltApiEndpoint}/${this.endpoint}`).pipe(
      mergeMap(rssFeeds => {
        // Create an array of observables for the results of each feed
        const detailRequests = rssFeeds.map(feed =>
          this.http.get<RssFeedResult[]>(`${environment.services.MltApiEndpoint}/${this.endpoint}/${feed.id}/Results`).pipe(
            map(results => ({...feed, results})) // Assign results to the 'results' property of each feed
          )
        );

        // Combine all observables into one
        return forkJoin(detailRequests);
      })
    );
  }


  // getAllRssFlux(): Observable<RssFeed[]> {
  //   let rssFeed = this.http.get<RssFeed[]>(this.endpoint);
  //
  //   rssFeed.subscribe(
  //     data => console.log(data),   // Log the response data when it arrives
  //     error => console.error(error)  // Log any error if the request fails
  //   );
  //
  //
  //
  //   return this.http.get<RssFeed[]>(this.endpoint);
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
