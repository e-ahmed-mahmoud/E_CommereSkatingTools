import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartItem, ShopCart } from '../../shared/models/cartModel';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {

  baseURl = environment.ApiUrl;

  private httpservice = inject(HttpClient);

  cart = signal<ShopCart | undefined>(undefined);
  private keyName = 'cart_id';

  cartItemsCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0) ?? 0;  //used as accumalation
  })

  totalsPrice = computed(() => {
    const cart = this.cart();
    if (!cart) return null;
    const productsPrice = cart.items.reduce((sum, cur) => sum + cur.price * cur.quantity, 0);
    const shippment = 10;
    const discount = 0;
    return { productsPrice, shippment, discount, total: productsPrice + shippment - discount };
  })

  getCart(id: string) {
    return this.httpservice.get<ShopCart>(`${this.baseURl}/cart?key=${id}`).pipe(
      map(cart => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  setCart(cart: ShopCart) {
    return this.httpservice.post<ShopCart>(`${this.baseURl}/cart`, cart).subscribe(value => {
      this.cart.set(value);
    });
  }

  addItemToCart(item: CartItem, quantity: number = 1) {
    const newCart = this.cart() ?? this.createCart();

    if (newCart.items.length === 0) {
      newCart.items = [{ ...item, quantity: quantity }];
      this.setCart(newCart);
      return;
    }
    const productIndex = newCart.items.findIndex(cur => cur.productId === item.productId);

    if (productIndex > -1) {
      newCart.items[productIndex].quantity += quantity;
    }
    else {
      newCart.items.push({ ...item, quantity: quantity });
    }
    this.setCart(newCart);
  }


  removeItemFromCart(itemId: string, quantity: number = 1) {
    const cart = this.cart();
    if (!cart) return;

    const productIndex = cart.items.findIndex(cur => cur.productId === itemId);

    if (productIndex < 0) return;

    if (cart.items[productIndex].quantity > quantity) {
      cart.items[productIndex].quantity -= quantity;
    }
    else {
      cart.items.splice(productIndex, 1);
    }

    if (cart.items.length === 0) {
      this.deleteCart();
    }
    else {
      this.setCart(cart);
    }
  }


  deleteCart() {
    const cartId = localStorage.getItem(this.keyName);
    if (!cartId) return;
    debugger;
    return this.httpservice.delete(`${this.baseURl}/cart?key=${cartId}`).subscribe({
      next: () => {
        debugger;
        localStorage.removeItem(this.keyName);
        this.cart.set(undefined);
      },
      error: err => console.log
    })
  }


  private createCart(): ShopCart {
    const newCart = new ShopCart();
    localStorage.setItem(this.keyName, newCart.id);
    return newCart;
  }
}

