import { AfterViewInit, Component, ElementRef, EventEmitter, inject, OnDestroy, OnInit, signal, ViewChild, viewChild } from '@angular/core';
import { HttpShopService } from '../../core/services/http-shop.service';
import { Product } from '../../shared/models/product';
import { ProductItem } from "../product-item/product-item";
import { MatDialog } from '@angular/material/dialog';
import { FilterDialog } from './filter-dialog/filter-dialog';
import { MatAnchor } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { FormsModule } from "@angular/forms";
import { ShopQueryParams } from '../../shared/models/shop-query-params';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { distinctUntilChanged, fromEvent, map, Subscription, switchMap, throttleTime } from 'rxjs';

@Component({
  selector: 'app-shop',
  imports: [ProductItem, MatAnchor, MatIcon, MatMenu, MatSelectionList, MatListOption, MatMenuTrigger, FormsModule, MatPaginator],
  templateUrl: './shop.html',
  styleUrl: './shop.css',
})
export class Shop implements AfterViewInit, OnInit, OnDestroy {

  products = signal<Pagination<Product[]> | undefined>(undefined);

  httpService = inject(HttpShopService);
  private matDialogService = inject(MatDialog);

  shopQueryParams = signal<ShopQueryParams>(new ShopQueryParams());

  sortOption = signal<any[]>([
    { name: 'name', value: 'name' },
    { name: 'Price: Low-High', value: 'priceAsc' },
    { name: 'Price: High-Low', value: 'priceDesc' }
  ]);

  pageSizeOptions = signal<number[]>([5, 10, 20, 50]);

  @ViewChild('search', { read: ElementRef }) searchInput?: ElementRef;
  private search$ = new Subscription();

  ngOnInit(): void {
    this.initializeShop();

  }

  ngAfterViewInit(): void {
    this.search$ = fromEvent(this.searchInput?.nativeElement, 'keyup').pipe(
      map((event: any) => event.target.value),
      throttleTime(200),
      distinctUntilChanged(),
      switchMap(value => {
        this.shopQueryParams.update(cur => ({ ...cur, search: value }));
        this.shopQueryParams.update(cur => ({ ...cur, pageNumber: 1 }));
        this.getProduct();
        return value;
      }),
    ).subscribe();
  }

  initializeShop() {
    this.getProduct();
    this.httpService.getTypes();
    this.httpService.getBrands();
  }

  onSortChange($event: MatSelectionListChange) {
    const selectionOption = $event.options[0];
    if (selectionOption) {
      this.shopQueryParams.update(cur => ({ ...cur, sort: selectionOption.value }));
      this.shopQueryParams.update(cur => ({ ...cur, pageNumber: 1 }))
      this.getProduct();

    }
  }

  // onSearchChange() {
  //   console.log();
  //   this.shopQueryParams.update(cur => ({ ...cur, pageNumber: 1 }));
  //   this.getProduct();
  // }

  getNextPage($event: PageEvent) {
    this.shopQueryParams.update(cur => ({ ...cur, pageNumber: $event.pageIndex + 1, pageSize: $event.pageSize }))
    this.getProduct();
  }

  getProduct() {
    this.httpService.getProducts(this.shopQueryParams()).subscribe({
      next: res => {
        this.products.set(res);
      },
      error: err => console.log
    });

  }

  showFiltersDialog() {
    const ref = this.matDialogService.open(FilterDialog, {
      minHeight: "500px",
      data: { brands: this.shopQueryParams().brands, types: this.shopQueryParams().types }
    });

    ref.afterClosed().subscribe({
      next: res => {
        if (res) {
          this.shopQueryParams.update(cur => ({ ...cur, brands: res.brands, types: res.types }));
          console.log(this.shopQueryParams());
          this.shopQueryParams.update(cur => ({ ...cur, pageNumber: 1 }))
          this.getProduct();
        }
      }

    }
    )
  }

  ngOnDestroy(): void {
    this.search$.unsubscribe();
  }


}
