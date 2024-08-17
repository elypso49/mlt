import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageRssComponent } from './page-rss/page-rss.component';

const routes: Routes = [
  { path: '', component: PageRssComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RssRoutingModule { }
