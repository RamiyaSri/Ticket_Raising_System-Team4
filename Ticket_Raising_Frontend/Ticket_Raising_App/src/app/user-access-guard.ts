import { CanActivateFn } from '@angular/router';

export const userAccessGuard: CanActivateFn = (route, state) => {
  let userName = sessionStorage.getItem('username');
  let role = sessionStorage.getItem('role');
  
  if (userName){
    return true;
  }
  else{
    return false;
  }
};
