import {NgModule, APP_INITIALIZER} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {RssModule} from './rss/rss.module';
import {PageHomeComponent} from './page-home/page-home.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
import {environment} from "../environments/environment";
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/fr';
import {SynologyModule} from "./synology/synology.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

registerLocaleData(localeFr);

export function initializeApp(): () => Promise<void> {
  return (): Promise<void> => {
    return new Promise((resolve) => {
      const env = (window as any).__env;
      if (env && env.services) {
        environment.services.MltApiEndpoint = env.services.MltApiEndpoint;
      }
      resolve();
    });
  };
}

@NgModule({
  declarations: [
    AppComponent,
    PageHomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RssModule,
    SynologyModule,
    NgbModule,
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule {
}
