import {Injectable} from '@angular/core';
import {forkJoin, map, Observable, of} from 'rxjs';
import {RssFeed} from "../core/models/rssFeed";
import {RssFeedResult} from "../core/models/RssFeedResult";
import {StateValue} from "../core/enums/StateValue";

@Injectable({
  providedIn: 'root'
})
export class SynologyService {
  constructor() {
  }

  downloadItems(rssFeedResults: RssFeedResult[]): Observable<RssFeedResult[]> {
    const observables: Observable<RssFeedResult>[] = rssFeedResults.map(item => this.downloadItem(item).pipe(map(() => item)));

    return forkJoin(observables);
  }

  downloadItem(rssFeedResult: RssFeedResult): Observable<RssFeedResult> {
    return of(rssFeedResult).pipe(
      map(downloadedItem => {
        downloadedItem.state |= StateValue.Downloaded;
        return downloadedItem;
      })
    );
  }
}
