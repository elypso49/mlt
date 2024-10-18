import {Component, OnInit} from '@angular/core';
import {ToastrService} from 'ngx-toastr';
import {RealdebridService} from '../../services/realdebrid.service';
import {SynologyService} from '../../services/synology.service';
import {RssFluxService} from "../../services/rss-flux.service";
import {RssFeed} from "../../core/models/RssFeed";
import {RssFeedResult} from "../../core/models/RssFeedResult";
import {StateValue} from "../../core/enums/StateValue";
import {tr} from "@faker-js/faker";

@Component({
  selector: 'app-page-rss',
  templateUrl: './page-rss.component.html',
  styleUrls: ['./page-rss.component.scss']
})
export class PageRssComponent implements OnInit {
  expandedFeedIndexes: number[] = [];
  rssFeeds: RssFeed[] = [];
  filterText: string = '';
  filteredFeeds: RssFeed[] = [];
  showNewElementModal: boolean = false;
  newRssFeed: RssFeed = {
    name: '',
    url: '',
    results: [],
    lastUpdate: undefined,
    category: 'Anime',
    destinationFolder: 'Animes',
    fileNameRegex: '',
    forceFirstSeasonFolder: true
  };


  constructor(
    private rssFluxService: RssFluxService,
    private realDebridService: RealdebridService,
    private synologyService: SynologyService,
    private toastr: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.InitializeList();
  }

  applyFilter(): void {
    if (this.filterText.trim() === '') {
      this.filteredFeeds = this.rssFeeds;
    } else {
      const filterLower = this.filterText.toLowerCase();
      this.filteredFeeds = this.rssFeeds?.filter(feed => feed.name.toLowerCase().includes(filterLower)) || null;
    }
  }

  toggleFeedExpansion(index: number): void {
    if (this.expandedFeedIndexes.includes(index)) {
      this.expandedFeedIndexes = this.expandedFeedIndexes.filter(i => i !== index);
    } else {
      this.expandedFeedIndexes.push(index);
    }
  }

  markAsNotProcessed(rssFeedResult: RssFeedResult): void {
    rssFeedResult.state = StateValue.NoProcessed;
    this.rssFluxService.updateRssFeedResult(rssFeedResult).subscribe(isSuccess => {
      if (isSuccess) {
        this.toastr.info('Queued for download', `${rssFeedResult.title}`);
      } else {
        this.toastr.error('Operation failed', 'Failure');
      }
    });
  }

  markAsDownloaded(rssFeedResult: RssFeedResult): void {
    rssFeedResult.state = StateValue.Downloaded;
    this.rssFluxService.updateRssFeedResult(rssFeedResult).subscribe(isSuccess => {
      if (isSuccess) {
        this.toastr.info('Flag as downloaded', `${rssFeedResult.title}`);
      } else {
        this.toastr.error('Operation failed', 'Failure');
        console.log('Reset failed.');
      }
    });
  }

  checkStateValue(result: RssFeedResult, expectedState: StateValue): boolean {
    return expectedState === Number(this.convertToEnum(result.state));
  }

  convertToEnum(value: any): StateValue | null {
    const strVal = value.toString();
    switch (strVal) {
      case "NoProcessed":
        return StateValue.NoProcessed;
      case "Downloaded":
        return StateValue.Downloaded;
    }
    const numberVal = Number(value);
    switch (numberVal) {
      case StateValue.NoProcessed:
        return StateValue.NoProcessed;
      case StateValue.Downloaded:
        return StateValue.Downloaded;
    }
    return null;
  }

  splitTags(fullString: string): string[] {
    return fullString.split('/');
  }

  getSeasonAndEpisode(rssFeed: RssFeed, rssFeedResult: RssFeedResult): string {
    const title = rssFeedResult.title!;
    const serieName = rssFeedResult.nyaaInfoHash?.trim() ? '' : `${rssFeedResult.tvShowName}/`;

    // Regex for matching SXXEYY pattern
    const fileNameRegex = /S(\d{2})E(\d{2})/i;
    // Regex for matching "SEASONXX"
    const seasonRegex = /SEASON\s*(\d+)/i;
    // Regex for matching multiple seasons like "Season 1+2" or "S01+02"
    const multipleSeasonsRegex = /(?:S|SEASON)\s*(\d{1,2})(?:\s*\+\s*(\d{1,2}))+/gi;
    // Regex for matching just the episode number in formats like "- 03"
    const episodeOnlyRegex = /-\s*(\d{1,2})/;
    // Regex for matching the whole season, e.g., "Season X" or "Batch"
    const wholeSeasonRegex = /SEASON\s*\d+|BATCH/i;
    // Regex for matching specials like "S02SP01-02"
    const specialsRegex = /S(\d{2})SP(\d{1,2})-(\d{1,2})/i;
    // Regex for matching OVA/OAV with an optional number
    const oavRegex = /(OAV|OVA)\s*(\d{1,2})?/i;

    // Clean the title to simplify matching (mimicking `CleanTitle()` from C#)
    const cleanTitle = this.cleanTitle(title);

    // Try to match the fileNameRegex for "SXXEYY" format
    let match = fileNameRegex.exec(cleanTitle);
    if (match) {
      return `${serieName}Season ${parseInt(match[1], 10).toString().padStart(2, '0')}/Episode ${parseInt(match[2], 10).toString().padStart(2, '0')}`;
    }

    // If the previous regex doesn't work, try to match a season-only format
    match = seasonRegex.exec(cleanTitle);
    if (match) {
      return `${serieName}Season ${parseInt(match[1], 10).toString().padStart(2, '0')}`;
    }

    // Check if it is multiple seasons defined
    let seasonsList = [];
    const multipleSeasonsPattern = /(?:S|SEASON)\s*(\d{1,2})/gi;
    let seasonsMatch;
    while ((seasonsMatch = multipleSeasonsPattern.exec(cleanTitle)) !== null) {
      const seasonNumber = parseInt(seasonsMatch[1], 10).toString().padStart(2, '0');
      seasonsList.push(`Season ${seasonNumber}`);
    }
    if (seasonsList.length > 0) {
      return seasonsList.join(', ');
    }

    // Check if it is a whole season batch
    if (wholeSeasonRegex.test(cleanTitle)) {
      return `${serieName}Season 00`;
    }

    // Match specials like "S02SP01-02" and return formatted as "Specials/SP 00"
    match = specialsRegex.exec(cleanTitle);
    if (match) {
      return `${serieName}Specials/SP ${parseInt(match[2], 10).toString().padStart(2, '0')}-${parseInt(match[3], 10).toString().padStart(2, '0')}`;
    }

    // Check if it's an OVA/OAV and return "Specials/OAV 00" or "Specials/OAV XX" if a number is provided
    match = oavRegex.exec(cleanTitle);
    if (match) {
      const oavNumber = match[2] ? parseInt(match[2], 10).toString().padStart(2, '0') : '00';
      return `${serieName}Specials/OAV ${oavNumber}`;
    }

    // Match episode-only regex to extract the episode number and assume Season 01
    match = episodeOnlyRegex.exec(cleanTitle);
    if (match) {
      return `${serieName}Season 01/Episode ${parseInt(match[1], 10).toString().padStart(2, '0')}`;
    }

    // Default to Season 01 if no other conditions are met
    return `${serieName}Season 01`;
  }

  private cleanTitle(title: string): string {
    return title
      .replace(/[^a-zA-Z0-9 -]/g, '')
      .trim()
      .toUpperCase();
  }

  refreshFeed(rssFeed: RssFeed): void {
    this.rssFluxService.refreshRssFeed(rssFeed).subscribe(isSuccess => {
      if (isSuccess) {
        this.toastr.info('Refreshed', `${rssFeed.name}`);
      } else {
        this.toastr.error('Operation failed', 'Failure');
      }
    });
  }

  saveFeed(rssFeed: RssFeed): void {
    this.rssFluxService.updateRssFeed(rssFeed).subscribe(isSuccess => {
      if (isSuccess) {
        this.toastr.info('Updated', `${rssFeed.name}`);
      } else {
        this.toastr.error('Operation failed', 'Failure');
      }
    });
  }

  openNewElementModal() {
    this.showNewElementModal = true;
  }

  // Method to close the modal
  closeNewElementModal() {
    this.showNewElementModal = false;
    this.resetNewFeed();
  }

  // Method to add a new RSS feed
  addNewRssFeed() {
    // Logic to add the new RSS feed
    if (this.newRssFeed.name && this.newRssFeed.url) {
      const newFeed = this.newRssFeed.name;

      this.rssFluxService.createRssFeed(this.newRssFeed).subscribe(isSuccess => {
        if (isSuccess) {
          this.toastr.info('Created', `${newFeed}`);
        } else {
          this.toastr.error('Operation failed', 'Failure');
        }
      });

      this.filteredFeeds.push({...this.newRssFeed});
      this.closeNewElementModal();
      this.resetNewFeed();
    }
  }


  protected readonly FeedProcessingState = StateValue;

  private InitializeList() {
    this.rssFluxService.getAllRssFlux().subscribe({
      next: (rssFlux: RssFeed[]) => {
        setTimeout(() => {
          rssFlux.forEach(feed => {
            feed.results = feed.results.sort((a, b) => new Date(b.createdDate!).getTime() - new Date(a.createdDate!).getTime());
          });

          this.rssFeeds = rssFlux.sort((a, b) => {
            const latestDateA = a.results.length > 0 ? new Date(a.results[0].createdDate!).getTime() : 0;
            const latestDateB = b.results.length > 0 ? new Date(b.results[0].createdDate!).getTime() : 0;

            if (latestDateA === latestDateB) {
              return a.name.localeCompare(b.name);
            }

            return latestDateB - latestDateA;
          });

          this.applyFilter();
        }, 1000);
      }
    });
  }

  private resetNewFeed() {
    this.newRssFeed = {
      name: '',
      url: '',
      results: [],
      lastUpdate: undefined,
      category: 'Anime',
      destinationFolder: 'Animes',
      fileNameRegex: '',
      forceFirstSeasonFolder: true
    };
  }
}
