import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { userAccessGuardGuard } from './user-access-guard-guard';

describe('userAccessGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => userAccessGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
