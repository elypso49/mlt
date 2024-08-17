import {Component, OnInit} from '@angular/core';
import {RealdebridService} from '../../services/realdebrid.service';
import {SynologyService} from '../../services/synology.service';
import {RssFluxService} from "../../services/rss-flux.service";
import {RssFeed} from "../../core/models/rssFeed";
import {RssFeedResult} from "../../core/models/RssFeedResult";
import {StateValue} from "../../core/enums/StateValue";

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-page-rss',
  templateUrl: './page-rss.component.html',
  styleUrls: ['./page-rss.component.scss']
})
export class PageRssComponent implements OnInit {
  expandedIndexes: number[] = [];
  debridingIndexes: number[] = [];
  debridingSubIndexes: number[] = [];
  downloadingIndexes: number[] = [];
  downloadingSubIndexes: number[] = [];
  rssData: RssFeed[] | null = null;

  constructor(private rssFluxService: RssFluxService, private realDebridService: RealdebridService, private synologyService: SynologyService) {
  }

  ngOnInit(): void {
    this.rssFluxService.getAllRssFlux().subscribe({
      next: (rssFlux: RssFeed[]) => {
        setTimeout(() => {
          this.rssData = rssFlux;
          if (this.rssData) {
            this.rssData.forEach((flux: RssFeed) => {
              if (flux && flux.results) {
                flux.results.forEach((item: RssFeedResult) => {
                  item.checked = true;
                });
              }
            });
          }

        }, 1000);
      }
    });
  }

  hasFlag(state: StateValue, flag: StateValue): boolean {
    return (state & flag) === flag;
  }


  areAllItemsSelected(items: RssFeedResult[]): boolean {
    return items.every(item => item.checked);
  }


  toggleItems(index: number): void {
    if (this.expandedIndexes.includes(index)) {
      this.expandedIndexes = this.expandedIndexes.filter(i => i !== index);
    } else {
      this.expandedIndexes.push(index);
    }
  }

  checkAll(rssFeed: RssFeed, index: number, currentValue: boolean) {
    rssFeed.results.forEach((item: RssFeedResult) => {
      item.checked = !currentValue;
    });
  }

  downloadAll(rssFeeds: RssFeed, index: number) {
    this.downloadingIndexes.push(index);

    const filteredItems: RssFeedResult[] = rssFeeds.results.filter((item: RssFeedResult) => item.checked && (item.state & StateValue.Debrided));

    let idx = 0;
    filteredItems.forEach((item: RssFeedResult) => {
      this.downloadOne(item, rssFeeds, index, idx++);
    });

    this.synologyService.downloadItems(filteredItems).subscribe(
      {
        next: (values) => {
          console.log(values);
        },
        complete: () => {
          this.downloadingIndexes = this.downloadingIndexes.filter(i => i !== index);
        }
      }
    )
  }

  downloadOne(item: RssFeedResult, rssFeed: RssFeed, index: number, subIndex: number) {
    if (item.state & StateValue.Debrided) {
      const idx = (index * 10) + subIndex;
      this.downloadingSubIndexes.push(idx);

      this.synologyService.downloadItem(item).subscribe(
        {
          next: (value) => {
            item.state |= StateValue.Downloaded;
          },
          complete: () => {
            this.downloadingSubIndexes = this.downloadingSubIndexes.filter(i => i !== idx);
          },
        }
      )
    }
  }

  debridAll(rssFeed: RssFeed, index: number) {
    this.debridingIndexes.push(index);

    const filteredItems: RssFeedResult[] = rssFeed.results.filter((item: RssFeedResult) => item.checked);

    this.realDebridService.debridItems(filteredItems).subscribe(
      {
        next: (values) => {
          console.log(values);
        },
        complete: () => {
          this.debridingIndexes = this.debridingIndexes.filter(i => i !== index);
        }
      }
    )
  }

  debridOne(item: RssFeedResult, index: number, subIndex: number) {
    const idx = (index * 10) + subIndex;
    this.debridingSubIndexes.push(idx);

    this.realDebridService.debridItem(item).subscribe(
      {
        next: (value) => {
          item.state |= StateValue.Debrided;
        },
        complete: () => {
          this.debridingSubIndexes = this.debridingSubIndexes.filter(i => i !== idx);
        },
      }
    )
  }

  protected readonly StateValue = StateValue;
}
