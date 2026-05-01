import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { LoadingService } from '../../core/services/Loading.service';
import { MatProgressBar } from '@angular/material/progress-bar';
import { CartService } from '../../core/services/cart.service';
import { MatBadgeModule } from "@angular/material/badge";
import { AccountService } from '../../core/services/account.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-header',
  imports: [MatIcon, MatButton, RouterLink, RouterLinkActive, MatProgressBar, MatBadgeModule, MatMenu, MatMenuItem, MatDivider, MatMenuTrigger],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  cartService = inject(CartService);
  isMenuOpen = false;

  loadingService = inject(LoadingService);

  accountService = inject(AccountService);
  router = inject(Router);
  snackbarError = inject(SnackbarService);

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.accountService.currentUser.set(null);
        this.router.navigateByUrl('/')
      },
      error: err => this.snackbarError.error(err)
    })

  }

}
