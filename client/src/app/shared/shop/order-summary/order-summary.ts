import { Component, inject, OnInit, signal } from '@angular/core';
import { MatAnchor } from "@angular/material/button";
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { RouterLink } from "@angular/router";
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-order-summary',
  imports: [MatAnchor, RouterLink, MatInput, MatFormField, MatLabel, CurrencyPipe],
  templateUrl: './order-summary.html',
  styleUrl: './order-summary.css',
})
export class OrderSummary implements OnInit {

  cartService = inject(CartService);

  hasItemInCart = this.cartService.cartItemsCount() > 0 ? true : false

  ngOnInit(): void {

  }


}
