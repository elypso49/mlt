import {DownloadStatus} from "../enums/DownloadStatus";

export class SynoFolder {
  name: string;
  path: string;
  folders: SynoFolder[];

  constructor(name: string = '', path: string = '', folders: SynoFolder[] = []) {
    this.name = name;
    this.path = path;
    this.folders = folders;
  }
}
