import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { Observable, of } from 'rxjs';
import { ShopCart } from '../../shared/models/cartModel';

@Injectable({
  providedIn: 'root',
})
export class InitService {

  private cartService = inject(CartService);

  init(): Observable<ShopCart | null> {
    const cart_id = localStorage.getItem('cart_id');
    const cart$ = cart_id ? this.cartService.getCart(cart_id) : of(null);
    return cart$;   //must return observable
  }

}
