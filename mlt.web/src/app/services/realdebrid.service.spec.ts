import { TestBed } from '@angular/core/testing';

import { RealdebridService } from './realdebrid.service';

describe('RealdebridService', () => {
  let service: RealdebridService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RealdebridService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
