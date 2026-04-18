import { Routes } from '@angular/router';
import { NotFound } from './shared/not-found/not-found';
import { ServerError } from './shared/server-error/server-error';

export const routes: Routes = [
    { path: '', loadComponent: () => import('./features/home/home').then(c => c.Home) },
    { path: 'shop', loadComponent: () => import('./features/shop/shop').then(c => c.Shop) },
    { path: "shop/:id", loadComponent: () => import('./features/product-details/product-details').then(c => c.ProductDetails) },
    { path: "not-found", component: NotFound },
    { path: "server-error", component: ServerError },
    { path: "**", redirectTo: "", pathMatch: "full" }
];
