import { HttpInterceptorFn } from '@angular/common/http';
import { AccountsService } from '../_services/accounts.service';
import { inject } from '@angular/core';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  var accountService = inject(AccountsService);

  if (accountService.currentUser()) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accountService.currentUser()?.authToken}`
      }
    });
  }

  return next(req);
};
