import { TestBed } from '@angular/core/testing';

import { ProductObserverService } from './product-observer.service';

describe('ProductObserverService', () => {
  let service: ProductObserverService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductObserverService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
