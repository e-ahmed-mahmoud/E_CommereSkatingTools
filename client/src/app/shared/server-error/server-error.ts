import { HttpErrorResponse } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  imports: [MatCard],
  templateUrl: './server-error.html',
  styleUrl: './server-error.css',
})
export class ServerError {

  error = signal<any>(undefined);

  isDevelopemntEnv = signal<boolean>(false);

  constructor(private router: Router) {
    this.error?.set(router.currentNavigation()?.extras.state?.['error']);
    this.isDevelopemntEnv.set(this.router.url.includes('localhost:'));
  }

}
