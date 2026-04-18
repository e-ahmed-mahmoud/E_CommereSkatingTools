import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { LoadingService } from '../services/LoadingService';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {

  const loadingService = inject(LoadingService);

  loadingService.busy();

  return next(req).pipe(
    delay(500),
    finalize(() => loadingService.idle())
  );
};
