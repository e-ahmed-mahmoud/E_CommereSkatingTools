import { Component, input, Input, signal } from '@angular/core';
import { Product } from '../../shared/models/product';
import { MatCard, MatCardContent, MatCardTitle, MatCardActions } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, NgClass } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-product-item',
  imports: [MatCard, MatIcon, MatButton, MatCardContent, CurrencyPipe, MatCardActions, RouterLink],
  templateUrl: './product-item.html',
  styleUrl: './product-item.css',
})
export class ProductItem {

  product = input.required<Product>();

}
