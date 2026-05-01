import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackbarSerivce = inject(SnackbarService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      console.log(err.status);
      if (err.status === 400) {
        if (err.error.errors) {
          const modelStateErrors: string[] = [];
          for (const key in err.error.errors) {
            if (err.error.errors[key]) {
              modelStateErrors.push(err.error.errors[key].errorCode + ' : ' + err.error.errors[key].errorMessage)
            }
          }
          throw modelStateErrors.flat();
        }
        else {
          snackbarSerivce.error(err.message);
        }
      }

      else if (err.status === 401) {
        snackbarSerivce.error('`not authrized user')
      }
      else if (err.status === 404) {
        router.navigateByUrl('/not-found')
      }
      else if (err.status === 500) {
        const navigationExtra: NavigationExtras = { state: { error: err.error } };
        router.navigateByUrl('/server-error', navigationExtra);
      }
      else {
        console.log(err);
      }

      return throwError(() => err);
    })
  );
};
