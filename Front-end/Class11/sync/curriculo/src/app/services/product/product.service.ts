import { Injectable } from '@angular/core';
import { Product } from 'src/app/models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  /** ATTRIBUTES **/
  private static productList: Product[] = [];

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  // static listProduct(): Product[] {}
}
