import { TestBed } from '@angular/core/testing';

import { DataReadingService } from './data-reading.service';

describe('DataReadingService', () => {
  let service: DataReadingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataReadingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
