import {Injectable} from '@angular/core';
import {forkJoin, map, Observable, of} from 'rxjs';
import {RssFeedResult} from "../core/models/RssFeedResult";
import {StateValue} from "../core/enums/StateValue";


@Injectable({
  providedIn: 'root'
})
export class RealdebridService {

  constructor() {
  }

  // debridItems(rssFeedResults: RssFeedResult[]): Observable<RssFeedResult[]> {
  //   const observables: Observable<RssFeedResult>[] = rssFeedResults.map(item => this.debridItem(item).pipe(map(() => item)));
  //
  //   return forkJoin(observables);
  // }
  //
  // debridItem(rssFeedResult: RssFeedResult): Observable<RssFeedResult> {
  //   return of(rssFeedResult).pipe(
  //     map(debridedItem => {
  //       debridedItem.state |= StateValue.Debrided;
  //       return debridedItem;
  //     })
  //   );
  // }
}
