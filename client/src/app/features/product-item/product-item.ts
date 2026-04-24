import { Component, EventEmitter, inject, input, Input, Output, signal } from '@angular/core';
import { Product } from '../../shared/models/product';
import { MatCard, MatCardContent, MatCardTitle, MatCardActions } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, NgClass } from '@angular/common';
import { RouterLink } from "@angular/router";
import { CartService } from '../../core/services/cart-service';
import { MapProductToCartItem } from '../../shared/models/cartModel';
import { SnackbarService } from '../../core/services/snackbar.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-item',
  imports: [MatCard, MatIcon, MatButton, MatCardContent, CurrencyPipe, MatCardActions, RouterLink],
  templateUrl: './product-item.html',
  styleUrl: './product-item.css',
})
export class ProductItem {

  product = input.required<Product>();

  @Output() additemToCartEvent = new EventEmitter<Product>();

  private cartService = inject(CartService);
  private snakbarService = inject(SnackbarService);

  onAddItemToCart() {
    const cartItem = MapProductToCartItem(this.product());
    if (cartItem)
      this.cartService.addItemToCart(cartItem, 1);
    else {
      this.snakbarService.error('not product to add to shop cart');
    }

  }
}
