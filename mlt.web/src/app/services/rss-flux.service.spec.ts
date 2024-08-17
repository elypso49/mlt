import { TestBed } from '@angular/core/testing';

import { RssFluxService } from './rss-flux.service';

describe('RssFluxService', () => {
  let service: RssFluxService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RssFluxService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
