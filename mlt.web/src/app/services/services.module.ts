import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RealdebridService} from './realdebrid.service';
// import {ShowrssService} from './showrss.service';
import {SynologyService} from './synology.service';
import {RssFluxService} from './rss-flux.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [RealdebridService, SynologyService, RssFluxService],
})
export class ServicesModule {
}
