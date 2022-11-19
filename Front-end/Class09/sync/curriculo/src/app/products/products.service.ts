import { Injectable } from '@angular/core';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  /** ATTRIBUTES **/
  private static product: Product = {} as Product;

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
}
