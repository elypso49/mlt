import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageRssComponent } from './page-rss.component';

describe('PageRssComponent', () => {
  let component: PageRssComponent;
  let fixture: ComponentFixture<PageRssComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PageRssComponent]
    });
    fixture = TestBed.createComponent(PageRssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
