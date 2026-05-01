import { CanDeactivateFn } from '@angular/router';
import { OrderSummary } from '../../shared/shop/order-summary/order-summary';
import { SnackbarService } from '../services/snackbar.service';
import { inject } from '@angular/core';

export const canDeactivatedGuard: CanDeactivateFn<OrderSummary> = (
  component,
  currentRoute,
  currentState,
  nextState,
) => {
  if (component as OrderSummary) {
    console.log(component);
    if (component.cartService.cartItemsCount() > 0) {
      return true
    }
    else {
      inject(SnackbarService).error("shop cart is empty");
      return false;
    }
  }
  return true;
};
