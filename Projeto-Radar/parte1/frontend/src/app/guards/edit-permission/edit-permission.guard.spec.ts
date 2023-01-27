import { TestBed } from '@angular/core/testing';

import { EditPermissionGuard } from './edit-permission.guard';

describe('EditPermissionGuard', () => {
  let guard: EditPermissionGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(EditPermissionGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
