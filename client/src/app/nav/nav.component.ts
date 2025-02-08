import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { AccountsService } from '../_services/accounts.service';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-arsh-nav',
  standalone: true,
  imports: [FormsModule, NgbDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountsService = inject(AccountsService)
  router = inject(Router);
  toastr = inject(ToastrService);

  collapsed: boolean = true;

  model: any = {};

  login() {
    this.accountsService
      .login(this.model)
      .subscribe({
        next: response => {
          this.router.navigateByUrl('/members');
        },
        error: error => this.toastr.error(error.error)
      });
  }

  logout() {
    this.accountsService.logout();
    this.router.navigateByUrl('/');
  }
}
