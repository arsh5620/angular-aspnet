import { Component, inject, input, OnInit, output } from '@angular/core';
import { JsonPipe } from '@angular/common';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountsService } from '../_services/accounts.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  accountService = inject(AccountsService);
  usersFromHomeModel = input.required<any>();
  cancelForm = output<boolean>();

  registerForm: FormGroup = new FormGroup({});

  usernameFormControl = new FormControl('', Validators.required);
  passwordFormControl = new FormControl('', [Validators.required, Validators.min(6), Validators.max(22)]);
  confirmPasswordFormControl = new FormControl('', this.compareToPassword(this.passwordFormControl));

  model: any = {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = new FormGroup(
      {
        username: this.usernameFormControl,
        password: this.passwordFormControl,
        confirmPassword: this.confirmPasswordFormControl
      }
    );
  }

  compareToPassword(sourceControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl) => {
      return sourceControl.value == control.value ? null : { isMatching: false };
    }
  }

  register() {
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe({
    //   next: response => {
    //     console.log(response);
    //     this.cancel();
    //   }, error: errorResponse => {
    //     console.log("Something went wrong: ", errorResponse)
    //   }
    // });
  }

  cancel() {
    this.cancelForm.emit(false);
  }
}
