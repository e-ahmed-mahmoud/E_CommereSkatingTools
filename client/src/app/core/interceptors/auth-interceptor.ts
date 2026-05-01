import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  let clonedReq = req.clone({
    withCredentials: true
  });

  return next(clonedReq);
};
