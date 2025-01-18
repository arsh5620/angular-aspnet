import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  http = inject(HttpClient);
  baseUrl = "http://localhost:5168/api/";

  login(model: any) {
    return this.http.post(this.baseUrl + "accounts/login", model);
  }

}
