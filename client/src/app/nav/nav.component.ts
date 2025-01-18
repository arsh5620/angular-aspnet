import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { AccountsService } from '../_services/accounts.service';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-arsh-nav',
  standalone: true,
  imports: [FormsModule, NgbDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountsService = inject(AccountsService)
  loggedIn:boolean = false;
  collapsed:boolean = true;

  model: any = {};
  login() {
    this.accountsService
      .login(this.model)
      .subscribe({
        next: response => {
          this.model = response;
          this.loggedIn = true;
        },
        error: error => console.log(error)
      });
  }
}
