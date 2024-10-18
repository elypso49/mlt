import {DownloadStatus} from "../enums/DownloadStatus";

export class SynoTask {
  destination: string = '';
  uri: string = '';
  title: string = '';
  id: string = '';
  status: DownloadStatus = DownloadStatus.Waiting;
  size: number = 0;
  sizeDownloaded: number = 0;
  createdDateTime?: Date;
  completedDateTime?: Date;

  constructor(init?: Partial<SynoTask>) {
    Object.assign(this, init);
  }
}
