import { Component, inject, OnInit, signal } from '@angular/core';
import { CartService } from '../../core/services/cart-service';
import { MatCard, MatCardHeader } from "@angular/material/card";
import { CartItem, ShopCart } from '../../shared/models/cartModel';
import { ShopCartItem } from "./shop-cart-item/shop-cart-item";
import { OrderSummary } from "../../shared/shop/order-summary/order-summary";

@Component({
  selector: 'app-cart',
  imports: [ShopCartItem, OrderSummary],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart implements OnInit {

  cartService = inject(CartService)

  ngOnInit(): void {
    //this.cartItems.set(this.cartService.cart() ?? []);

  }
}
