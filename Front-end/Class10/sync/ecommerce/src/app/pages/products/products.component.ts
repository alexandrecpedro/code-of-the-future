import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ObserverProductService } from 'src/app/services/observer-product/observer-product.service';
import { ProductService } from 'src/app/services/product/product.service';
import { Product } from '../../models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  product: Product = {} as Product;
  products: Product[] = ProductService.searchProducts();

  /** CONSTRUCTOR **/
  constructor(
    private router: Router,
    private productObserverService: ObserverProductService
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    // this.products = ProductService.listProducts();
  }

  addToCart(product: Product): void {
    ProductService.addProduct(product);
    this.products = ProductService.searchProducts();
    this.productObserverService.updateStock();
  }
}
