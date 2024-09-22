import { TestBed } from '@angular/core/testing';

import { WarningListenerService } from './warning-listener-service.service';
import { provideHttpClient } from '@angular/common/http';
import { HttpClientTestingModule, provideHttpClientTesting } from '@angular/common/http/testing';

describe('WarningListenerServiceService', () => {
  let service: WarningListenerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting(), HttpClientTestingModule]
    });;
    service = TestBed.inject(WarningListenerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
