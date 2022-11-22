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
  static searchProducts(): Product[] {
    return ProductService.productList;
  }

  static searchProductById(id: Number): Product {
    let product: Product = {} as Product;
    let findProduct = ProductService.productList.find(product => product.id === id);
    if (findProduct !== undefined) product = findProduct;
    return product;
  }

  static addProduct(product: Product): void {
    product.id = ProductService.searchProducts().length + 1;
    ProductService.productList.push(product);
  }

  static updateProduct(product: Product): void {
    for (let i=0; i<ProductService.productList.length; i++) {
      let productDb = ProductService.productList[i];
      if (productDb.id === product.id) {
        productDb = {...product};
        break;
      }
    }
  }

  static deleteProduct(product: Product): void {
    let newList = [];
    for (let i=0; i<ProductService.productList.length; i++) {
      let productDb = ProductService.productList[i];
      if (productDb.id !== product.id) {
        newList.push(productDb);
      }
    }

    ProductService.productList = newList;
  }
}
