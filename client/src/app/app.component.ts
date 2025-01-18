import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [NavComponent]
})

export class AppComponent implements OnInit {
  title = 'client';
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.http.get('http://localhost:5168/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })

    this.http.get('http://localhost:5168/api/users/1').subscribe(response => {
      console.log("something");
    })
  }
}
