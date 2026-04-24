import { nanoid } from 'nanoid'
import { Product } from './product';

export interface CartModel {

    id: string;
    items: CartItem[];

}

export interface CartItem {
    productId: string;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export class ShopCart implements CartModel {
    id: string = nanoid();
    items: CartItem[] = [];

}

export function MapProductToCartItem(product: Product): CartItem {
    return { ...product, productId: product.id, productName: product.name, quantity: 0 }
}

