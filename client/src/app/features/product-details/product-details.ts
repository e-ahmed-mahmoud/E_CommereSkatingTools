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

@Component({
  selector: 'app-product-details',
  imports: [CurrencyPipe, MatAnchor, MatIcon, MatFormField, MatLabel, MatInput, MatDivider],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails implements OnInit {

  httpService = inject(HttpShopService);

  activatedRoute = inject(ActivatedRoute);

  product = signal<Product | undefined>(undefined);

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.httpService.getProductById(id).subscribe({
        next: response => {
          this.product.set(response);
        },
        error: (err) => console.log
      });
    }

  }

}
