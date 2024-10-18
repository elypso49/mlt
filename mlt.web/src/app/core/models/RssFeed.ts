import {RssFeedResult} from "./RssFeedResult";

export interface RssFeed {
  id?: string; // Optional property
  name: string;
  url: string;
  lastUpdate: Date | undefined; // Use Date type for DateTime
  forceFirstSeasonFolder: boolean;
  fileNameRegex : string;
  category: string;
  destinationFolder: string;
  results: RssFeedResult[];
}
