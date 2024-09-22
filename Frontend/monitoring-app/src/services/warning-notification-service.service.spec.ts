import { TestBed } from '@angular/core/testing';

import { WarningNotificationService } from './warning-notification-service.service';

describe('WarningNotificationService', () => {
  let service: WarningNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WarningNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
