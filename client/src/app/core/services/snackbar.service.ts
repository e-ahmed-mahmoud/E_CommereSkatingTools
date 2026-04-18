import { inject, Injectable } from '@angular/core';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {

  snakbar = inject(MatSnackBar);

  error(message: string) {
    this.snakbar.open(message, 'Close', {
      duration: 5000,
      panelClass: ['snack-error']
    });
  }
  success(message: string) {
    this.snakbar.open(message, 'Close', {
      duration: 5000,
      panelClass: ['snack-success']
    });
  }

}
