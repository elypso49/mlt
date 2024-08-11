import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {RssRoutingModule} from './rss-routing.module';
import {PageRssComponent} from './page-rss/page-rss.component';
import {ServicesModule} from '../services/services.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PageRssComponent
  ],
  imports: [
    CommonModule,
    RssRoutingModule,
    HttpClientModule,
    ServicesModule,
    ReactiveFormsModule,
    FormsModule
  ],
})
export class RssModule {
}


