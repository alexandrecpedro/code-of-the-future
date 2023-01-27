import { TestBed } from '@angular/core/testing';

import { ClientObserverService } from './client-observer.service';

describe('ClientObserverService', () => {
  let service: ClientObserverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientObserverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
