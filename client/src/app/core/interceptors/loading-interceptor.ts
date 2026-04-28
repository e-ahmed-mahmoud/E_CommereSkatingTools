import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { LoadingService } from '../services/Loading.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {

  const loadingService = inject(LoadingService);

  loadingService.busy();

  return next(req).pipe(
    delay(300),
    finalize(() => loadingService.idle())
  );
};
