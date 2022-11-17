import { TestBed } from '@angular/core/testing';

import { ExtractService } from './extract.service';

describe('ExtractService', () => {
  let service: ExtractService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExtractService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
