import { TestBed } from '@angular/core/testing';

import { WarningSerivce } from './warning.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('WarningService', () => {
  let service: WarningSerivce;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting(), HttpClientTestingModule]
    });
    service = TestBed.inject(WarningSerivce);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
