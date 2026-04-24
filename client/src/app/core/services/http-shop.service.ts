import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ShopQueryParams } from '../../shared/models/shop-query-params';
import { reportUnhandledError } from 'rxjs/internal/util/reportUnhandledError';
import { map, pipe } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class HttpShopService {
  baseURL = environment.ApiUrl;

  httpService = inject(HttpClient);

  brands = signal<string[]>([]);
  types = signal<string[]>([]);

  getProducts(queryParams: ShopQueryParams) {
    let params = new HttpParams();
    if (queryParams.brands.length > 0) {
      params = params.append('brands', queryParams.brands.join(','));
    }
    if (queryParams.types.length > 0) {
      params = params.append('types', queryParams.types.join(','));
    }
    if (queryParams.sort) {
      params = params.append('sort', queryParams.sort);
    }

    if (queryParams.search) {
      params = params.append('search', queryParams.search);
    }

    params = params.append('pageSize', queryParams.pageSize);
    params = params.append('pageNumber', queryParams.pageNumber);

    console.log({ params });

    return this.httpService.get<Pagination<Product[]>>(`${this.baseURL}/products`, { params });
  }

  getProductById(id: string) {
    return this.httpService.get<Product>(`${this.baseURL}/products/${id}`);
  }

  getBrands() {
    if (this.brands().length > 0)
      return;
    this.httpService.get<string[]>(`${this.baseURL}/products/getBrands`).subscribe({
      next: resposne => this.brands.set(resposne),
      error: err => console.log
    })
  }

  getTypes() {
    if (this.types().length > 0)
      return;
    this.httpService.get<string[]>(`${this.baseURL}/products/getTypes`).subscribe({
      next: resposne => this.types.set(resposne),
      error: err => console.log
    })
  }


}
