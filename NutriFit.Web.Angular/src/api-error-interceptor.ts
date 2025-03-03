import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { ToasterService } from './shared/services/toaster-service';

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const toasterService = inject(ToasterService);
  return next(req).pipe(
    catchError((error) => {
      if (error instanceof HttpErrorResponse) {
        let errorMessage = 'Ein unerwarteter Fehler ist aufgetreten.';

        if (error.error && typeof error.error === 'object') {
          errorMessage = error.error.message || errorMessage;
        }

        toasterService.showError(
          errorMessage,
          error.status + ' - ' + error.statusText
        );
      }
      return throwError(() => error);
    })
  );
};
