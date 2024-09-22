import { TestBed } from '@angular/core/testing';

import { LivedataService } from './livedata.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('LivedataService', () => {
  let service: LivedataService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting(), HttpClientTestingModule]
    });
    service = TestBed.inject(LivedataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
