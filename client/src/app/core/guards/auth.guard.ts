import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map, of, retry } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {

  const accountService = inject(AccountService);
  const router = inject(Router);

  if (accountService.currentUser()) {
    return of(true);
  }
  else {
    return accountService.isAuthenticated().pipe(
      map(val => {
        if (val.isAuthenticated) {
          return true;
        }
        else {
          router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
          return false;
        }
      })
    )
  }

}
