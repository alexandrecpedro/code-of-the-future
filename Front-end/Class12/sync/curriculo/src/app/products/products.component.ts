import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  product: Product = {} as Product;
  products: Product[] = [
    { id: 1, name: "PlayStation 5", description: "PlayStation 5", value: 5604.98 },
    { id: 2, name: "MacBook", description: "MacBook Pro 13 mid-2012", value: 4673.99 },
  ];

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  ngOnInit(): void {
    // this.products = ProductService.listProducts();
  }

  addProduct(): void {
    this.products.push({
      id: this.products.length + 1,
      name: this.product.name,
      description: this.product.description,
      value: this.product.value
    })
  }

  editProduct(productId: Number): void {
    let editProduct = this.products.find(product => product.id === productId);
    editProduct ? (this.product = editProduct) : null;
  }

  deleteProduct(productId: Number): Product[] {
    this.products.splice(Number(productId), 1);
    return this.products;
  }
}
