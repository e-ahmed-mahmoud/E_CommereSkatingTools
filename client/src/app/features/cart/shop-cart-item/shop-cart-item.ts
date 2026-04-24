import { Component, inject, input, Pipe, signal } from '@angular/core';
import { CartItem } from '../../../shared/models/cartModel';
import { RouterLink } from "@angular/router";
import { MatIcon } from "@angular/material/icon";
import { MatIconButton, MatAnchor } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { CartService } from '../../../core/services/cart-service';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-shop-cart-item',
  imports: [RouterLink, MatIconButton, MatIcon, CurrencyPipe, MatAnchor, DatePipe, FormsModule],
  templateUrl: './shop-cart-item.html',
  styleUrl: './shop-cart-item.css',
})
export class ShopCartItem {

  cartService = inject(CartService);

  cartItem = input.required<CartItem>();

  deliveryDate = new Date();

  onCartItemAdd() {
    this.cartService.addItemToCart(this.cartItem());
  }

  onCartItemRemove() {
    this.cartService.removeItemFromCart(this.cartItem().productId);
  }
  onCartItemDelete() {
    this.cartService.removeItemFromCart(this.cartItem().productId, this.cartItem().quantity);
  }

}
