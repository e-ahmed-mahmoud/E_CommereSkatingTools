import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../../core/services/account.service';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { MatCard } from "@angular/material/card";
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-login',
  imports: [MatCard, ReactiveFormsModule, MatInput, MatFormField, MatLabel, MatButton],
  templateUrl: './login.html',
  styleUrl: './login.css',
})

export class Login {

  private fb = inject(FormBuilder);

  private accountService = inject(AccountService);
  snackbarService = inject(SnackbarService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  returnedUrl = signal<string>('/shop');

  constructor() {
    const param = this.activatedRoute.snapshot.queryParams['returnUrl'];

    if (param) this.returnedUrl.set(param);
  }

  loginForm = this.fb.group({
    'email': ['', [Validators.required, Validators.email]],
    'password': ['', [Validators.required]]
  })

  onSubmit() {
    if (this.loginForm.valid) {
      this.accountService.login(this.loginForm.value).pipe(
        switchMap(() => this.accountService.getUserInfo())
      ).subscribe({
        next: () => {
          this.router.navigateByUrl(this.returnedUrl());
        },
        error: err => this.snackbarService.error(err)
      });
    }
  }
}