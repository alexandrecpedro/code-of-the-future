import { TestBed } from '@angular/core/testing';

import { LoggedService } from './logged.service';

describe('LoggedService', () => {
  let service: LoggedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoggedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
