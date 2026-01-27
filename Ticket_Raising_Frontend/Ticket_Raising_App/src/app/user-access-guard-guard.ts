import { CanActivateFn } from '@angular/router';

export const userAccessGuardGuard: CanActivateFn = (route, state) => {
  return true;
};
