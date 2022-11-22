import { TestBed } from '@angular/core/testing';

import { ObserverProductService } from './observer-product.service';

describe('ObserverProductService', () => {
  let service: ObserverProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ObserverProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
