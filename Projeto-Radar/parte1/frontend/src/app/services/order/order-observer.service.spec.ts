import { TestBed } from '@angular/core/testing';

import { OrderObserverService } from './order-observer.service';

describe('OrderObserverService', () => {
  let service: OrderObserverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderObserverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
