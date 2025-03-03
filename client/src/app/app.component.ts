import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountsService } from './_services/accounts.service';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [NavComponent, RouterOutlet]
})

export class AppComponent implements OnInit {
  accountService = inject(AccountsService);

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    var userString = localStorage.getItem('user');

    if (!userString) return;
    const user = JSON.parse(userString);

    this.accountService.currentUser.set(user);
  }
}
