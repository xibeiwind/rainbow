import { TestBed, async, inject } from '@angular/core/testing';

import { SysAdminAuthGuard } from './sys-admin-auth.guard';

describe('SysAdminAuthGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SysAdminAuthGuard]
    });
  });

  it('should ...', inject([SysAdminAuthGuard], (guard: SysAdminAuthGuard) => {
    expect(guard).toBeTruthy();
  }));
});
