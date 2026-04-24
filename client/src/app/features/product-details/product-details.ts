import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpShopService } from '../../core/services/http-shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { MatAnchor } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatFormField, MatLabel } from "@angular/material/select";
import { MatInput } from '@angular/material/input';
import { MatDivider } from "@angular/material/divider";
import { CartService } from '../../core/services/cart-service';
import { MapProductToCartItem } from '../../shared/models/cartModel';
import { SnackbarService } from '../../core/services/snackbar.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  imports: [CurrencyPipe, MatAnchor, MatIcon, MatFormField, MatLabel, MatInput, MatDivider, FormsModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails implements OnInit {

  httpService = inject(HttpShopService);

  activatedRoute = inject(ActivatedRoute);
  private cartService = inject(CartService);

  private snakbarService = inject(SnackbarService);

  product = signal<Product | null>(null);
  quantity = signal<number>(0);
  quantityInCart = signal<number>(0);

  ngOnInit(): void {

    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.httpService.getProductById(id).subscribe({
        next: response => {
          this.product.set(response);
          if (this.product() && this.cartService.cart()) {
            const item = this.cartService.cart()?.items.find(cur => cur.productId === this.product()?.id);
            this.quantityInCart.set(item?.quantity ?? 0);
            this.quantity.set(this.quantityInCart());
          }
        },
        error: (err) => this.snakbarService.error(err)
      });
    }
  }

  onUpdateItemToCart() {
    const product = this.product();
    if (!product) return;

    if (this.quantity() < this.quantityInCart()) {
      this.cartService.removeItemFromCart(product.id, (this.quantityInCart() - this.quantity()))
      this.quantityInCart.update(cur => cur - (cur - this.quantity()));
    }
    else {
      this.cartService.addItemToCart(MapProductToCartItem(product), (this.quantity() - this.quantityInCart()))
      this.quantityInCart.update(cur => cur + (this.quantity() - this.quantityInCart()));
    }

  }

}


