import { TestBed } from '@angular/core/testing';

import { SynologyService } from './synology.service';

describe('SynologyService', () => {
  let service: SynologyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SynologyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
