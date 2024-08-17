import {RssFeedResult} from "./RssFeedResult";

export interface RssFeed {
  id?: string; // Optional property
  name: string;
  url: string;
  lastUpdate: Date; // Use Date type for DateTime
  category: string;
  destinationFolder: string;
  results: RssFeedResult[];
}
