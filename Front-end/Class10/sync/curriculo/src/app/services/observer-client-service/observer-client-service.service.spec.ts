import { TestBed } from '@angular/core/testing';

import { ObserverClientServiceService } from './observer-client-service.service';

describe('ObserverClientServiceService', () => {
  let service: ObserverClientServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObserverClientServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
