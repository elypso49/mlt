import {LOCALE_ID, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {RssRoutingModule} from './rss-routing.module';
import {PageRssComponent} from './page-rss/page-rss.component';
import {ServicesModule} from '../services/services.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

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
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right', // Display toast in the top right corner
      timeOut: 3000, // Set the timeout for the toast
    }),
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'fr' }
  ],
})
export class RssModule {
}


