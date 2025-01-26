import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);

  users: any;
  registerNow: boolean = false;

  ngOnInit(): void {
    this.getUsers();
  }

  toggleRegisterNow() {
    this.registerNow = !this.registerNow;
  }

  cancelRegistrationForm(value: boolean) {
    if (!value) {
      this.registerNow = value;
    }
  }

  getUsers() {
    this.http.get('http://localhost:5168/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log("Something went wrong: " + error);
    })
  }
}
