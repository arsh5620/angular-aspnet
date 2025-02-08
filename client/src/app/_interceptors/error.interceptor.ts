import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  var result = next(req);
  var toastr = inject(ToastrService);

  return result.pipe(
    catchError(error=>{
      if (error){
        toastr.error(error.status);
      }

      throw error;
    })
  );
};
