import { TestBed } from '@angular/core/testing';

import { FormLeaveGuard } from './form-leave.guard';

describe('FormLeaveGuard', () => {
  let guard: FormLeaveGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(FormLeaveGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
