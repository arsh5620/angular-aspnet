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
export class HomeComponent {
  http = inject(HttpClient);

  users: any;
  registerNow: boolean = false;

  toggleRegisterNow() {
    this.registerNow = !this.registerNow;
  }

  cancelRegistrationForm(value: boolean) {
    if (!value) {
      this.registerNow = value;
    }
  }
}
