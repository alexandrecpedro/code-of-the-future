import { TestBed } from '@angular/core/testing';

import { ObserverClientService } from './observer-client.service';

describe('ObserverClientService', () => {
  let service: ObserverClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObserverClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
