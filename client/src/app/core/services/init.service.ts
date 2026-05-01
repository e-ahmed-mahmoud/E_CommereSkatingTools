import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { catchError, forkJoin, Observable, of } from 'rxjs';
import { ShopCart } from '../../shared/models/cartModel';
import { AccountService } from './account.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class InitService {

  private cartService = inject(CartService);
  private accountService = inject(AccountService);
  private router = inject(Router);
  init() {
    const cart_id = localStorage.getItem('cart_id');
    const cart$ = cart_id ? this.cartService.getCart(cart_id) : of(null);

    const user$ = this.accountService.getUserInfo().pipe(
      catchError((res) => {
        console.log(res);
        return this.router.navigateByUrl("/account/login");
      })
    );

    return forkJoin({ cart: cart$, user: user$ });   //must return observable
  }

}
