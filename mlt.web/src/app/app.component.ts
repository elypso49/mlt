import {Component} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'mlt.web';

  links = [
    {title: 'Home', fragment: ''},
    {title: 'RSS', fragment: 'rss'}
  ];

  constructor(public route: ActivatedRoute) {
  }
}
