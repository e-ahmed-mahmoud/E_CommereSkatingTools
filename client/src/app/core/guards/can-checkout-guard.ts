import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { CartService } from '../services/cart.service';
import { SnackbarService } from '../services/snackbar.service';

export const canCheckoutGuard: CanActivateFn = (route, state) => {

  const cartService = inject(CartService);
  const snackbarService = inject(SnackbarService);

  if (cartService.cartItemsCount() < 1) {
    snackbarService.error("your cart is empty");
    return false;
  }
  return true;

};
