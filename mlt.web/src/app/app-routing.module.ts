import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageHomeComponent } from './page-home/page-home.component';
import { PageRssComponent } from './rss/page-rss/page-rss.component';

const routes: Routes = [
  { path: '', component: PageHomeComponent },
  { path: 'rss', component: PageRssComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
