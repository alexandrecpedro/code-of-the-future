import { Injectable } from '@angular/core';
import { ProductService } from '../product/product.service';

@Injectable({
  providedIn: 'root'
})
export class ObserverProductService {
  /** ATTRIBUTES **/
  quantity: Number = 0;

  /** CONSTRUCTOR **/
  constructor() {
    this.updateStock();
  }

  /** METHODS **/
  updateStock(): void {
    console.log("Entered the method");
    this.quantity = ProductService.searchProducts().length;
  }
}
