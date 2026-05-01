import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { Router } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';
import { MatCard } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { JsonPipe } from '@angular/common';
import { TextInput } from "../../../shared/forms/text-input/text-input";

@Component({
  selector: 'app-register',
  imports: [MatCard, MatButton, ReactiveFormsModule, JsonPipe, TextInput],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  fb = inject(FormBuilder);
  snackbarService = inject(SnackbarService);

  accountService = inject(AccountService);

  routerService = inject(Router);

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&].{6,}$')]]
  })

  validationErrors = signal<string[]>([]);

  onSubmit() {
    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe({
        next: () => {
          this.snackbarService.success("user registered successfully");
          this.routerService.navigateByUrl('/account/login');
        },
        error: err => { this.validationErrors.set(err) }
      })
    }
  }

}
