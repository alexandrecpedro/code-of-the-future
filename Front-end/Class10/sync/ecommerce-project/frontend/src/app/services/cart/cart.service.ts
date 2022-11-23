import { Injectable } from '@angular/core';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  /** ATTRIBUTES **/
  cartData: any = {
    products: [],
    total: 0,
  };

  cartDataObs$ = new BehaviorSubject(this.cartData);
  
  /** CONSTRUCTOR **/
  constructor(
    private _notification: NzNotificationService,
    private _api: ApiService,
  ) {
    let localCartData = localStorage.getItem('cart');
    if (localCartData) {
      this.cartData = JSON.parse(localCartData);
    }
    this.cartDataObs$.next(this.cartData);
  }

  /** METHODS **/
  submitCheckout(userId: number, cart: Object): any {
    return this._api.postTypeRequest(`orders/create`, {
      userId: userId.toString(),
      cart: cart,
    });
  }

  addProduct(params: any): void {
    const { id, price, quantity, image, title, maxQuantity } = params;
    const product = { id, price, quantity, image, title, maxQuantity };

    if (!this.isProductInCart(id)) {
      if (quantity) this.cartData.products.push(product);
      else this.cartData.products.push({ ...product, quantity: 1 });
    } else {
      let updatedProducts = [...this.cartData.products];
      let productIndex = updatedProducts.findIndex((product) => product.id === id);
      let product = updatedProducts[productIndex];

      if (quantity) {
        updatedProducts[productIndex] = {
          ...product,
          quantity: quantity,
        };
      } else {
        updatedProducts[productIndex] = {
          ...product,
          quantity: product.quantity + 1,
        };
      }

      this.cartData.products = updatedProducts;
    }

    this.cartData.total = this.getCartTotal();
    this._notification.create(
      'success',
      'Product added to cart',
      `${title} was successfully added to the cart`
    );
    this.cartDataObs$.next({...this.cartData});
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }

  updateCart(id: number, quantity: number): void {
    let updatedProducts = [...this.cartData.products];
    let productIndex = updatedProducts.findIndex((product) => product.id === id);

    updatedProducts[productIndex] = {
      ...updatedProducts[productIndex],
      quantity: quantity,
    };

    this.cartData.products = updatedProducts;
    this.cartData.total = this.getCartTotal();
    this.cartDataObs$.next({...this.cartData});
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }

  removeProduct(id: number): void {
    let updatedProducts = this.cartData.products.filter((product: { id: number; }) => product.id !== id);

    this.cartData.products = updatedProducts;
    this.cartData.total = this.getCartTotal();
    this.cartDataObs$.next({...this.cartData});
    localStorage.setItem('cart', JSON.stringify(this.cartData));

    this._notification.create(
      'success',
      'Removed successfully',
      'The selected item was removed from the cart successfully'
    );
  }

  clearCart(): void {
    this.cartData = {
      products: [],
      total: 0,
    };
    this.cartDataObs$.next({...this.cartData});
    localStorage.setItem('cart', JSON.stringify(this.cartData));
  }

  getCartTotal(): number {
    let totalSum = 0;
    this.cartData.products.forEach(
      (product: { price: number; quantity: number; }) => totalSum += product.price * product.quantity
    );
    return totalSum;
  }

  isProductInCart(id: number): boolean {
    return (this.cartData.products.findIndex((product: { id: number; }) => product.id === id)) !== -1;
  }
}
