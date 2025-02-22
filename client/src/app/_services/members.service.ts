import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Member } from '../_models/member';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  private members: Member[] = [];
  private apiUrl = environment.apiUrl;

  getMembers() {
    return this.http.get<Member[]>(this.apiUrl + "users")
  }

  getMember(username: string) {
    return this.http.get<Member>(this.apiUrl + "users/" + username);
  }

  updateMember(user: Member) {
    return this.http.put(this.apiUrl + "users/", user);
  }
}
