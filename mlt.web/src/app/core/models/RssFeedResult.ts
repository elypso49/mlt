import {StateValue} from '../enums/StateValue'

export interface RssFeedResult {
  id?: string; // Optional
  rssFeedId: string;
  title?: string; // Optional
  link?: string; // Optional
  description?: string; // Optional
  publishDate: Date; // Use Date type for DateTime
  createdDate?: Date; // Optional, default to current date if not provided
  state: StateValue; // Use enum type
  updatedDate?: Date; // Optional

  // TV fields
  tvShowId?: string; // Optional
  tvExternalId?: string; // Optional
  tvShowName?: string; // Optional
  tvEpisodeId?: string; // Optional
  tvRawTitle?: string; // Optional
  tvInfoHash?: string; // Optional

  // Nyaa fields
  nyaaSeeders?: number; // Optional
  nyaaLeechers?: number; // Optional
  nyaaDownloads?: number; // Optional
  nyaaInfoHash?: string; // Optional
  nyaaCategoryId?: string; // Optional
  nyaaCategory?: string; // Optional
  nyaaSize?: string; // Optional
  nyaaComments?: number; // Optional
  nyaaTrusted?: string; // Optional
  nyaaRemake?: string; // Optional

  // Web fields
  checked: boolean;
}
