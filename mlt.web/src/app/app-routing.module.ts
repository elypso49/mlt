import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageHomeComponent } from './page-home/page-home.component';
import { PageRssComponent } from './rss/page-rss/page-rss.component';
import {PageSynologyComponent } from './synology/page-synology/page-synology.component'

const routes: Routes = [
  { path: '', component: PageRssComponent },
  { path: 'syno', component: PageSynologyComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
