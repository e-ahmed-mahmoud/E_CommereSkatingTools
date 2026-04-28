import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'
import { RouterLink, RouterLinkActive } from "@angular/router";
import { LoadingService } from '../../core/services/Loading.service';
import { MatProgressBar } from '@angular/material/progress-bar';
import { CartService } from '../../core/services/cart.service';
import { MatBadgeModule } from "@angular/material/badge";

@Component({
  selector: 'app-header',
  imports: [MatIcon, MatButton, RouterLink, RouterLinkActive, MatProgressBar, MatBadgeModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  cartService = inject(CartService);

  loadingService = inject(LoadingService);
}
