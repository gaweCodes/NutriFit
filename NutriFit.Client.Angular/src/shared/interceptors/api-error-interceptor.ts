import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, throwError } from 'rxjs';
import { ToasterService } from '../services/toaster-service';

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const toasterService = inject(ToasterService);
  return next(req).pipe(
    tap(() => toasterService.clear()),
    catchError((error) => {
      if (error instanceof HttpErrorResponse) {
        let errorMessage = 'Ein unerwarteter Fehler ist aufgetreten.';

        if (error.error && typeof error.error === 'object') {
          errorMessage = error.error.error || errorMessage;
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
