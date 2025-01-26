import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  http = inject(HttpClient);
  baseUrl = "http://localhost:5168/api/";
  currentUser = signal<User | null>(null)

  login(model: any) {
    return this.http.post<User>(this.baseUrl + "accounts/login", model).pipe(
      map(user=>{
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.set(user);
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "accounts/register", model).pipe(
      map(user=>{
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.set(user);

        return user;
      })
    );
  }

  logout(){
      localStorage.removeItem('item');
      this.currentUser.set(null);
  }
}
