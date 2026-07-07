import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

function withToken(req: HttpRequest<unknown>, token: string): HttpRequest<unknown> {
  return req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
}

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const token = authService.getToken();
  const authRequest = token ? withToken(req, token) : req;

  return next(authRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      const isAuthEndpoint = req.url.includes('/auth/');

      // 401 na običnom API pozivu → pokušaj refresh pa ponovi zahtjev
      if (error.status === 401 && !isAuthEndpoint && authService.getRefreshToken()) {
        return authService.refreshSession().pipe(
          switchMap(() => next(withToken(req, authService.getToken()!))),
          catchError(refreshError => {
            // Refresh nije uspio → sesija je istekla, vrati korisnika na login
            authService.clearSession();
            router.navigate(['/login']);
            return throwError(() => refreshError);
          })
        );
      }

      return throwError(() => error);
    })
  );
};
