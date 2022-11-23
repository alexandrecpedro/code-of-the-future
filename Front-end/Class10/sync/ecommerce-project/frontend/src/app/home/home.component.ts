import { Component, HostListener, OnInit, ViewEncapsulation } from '@angular/core';
import { finalize } from 'rxjs';
import { CartService } from '../services/cart/cart.service';
import { ProductService } from '../services/product/product.service';
import { Product } from '../shared/models/product.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class HomeComponent implements OnInit {
  /** ATTRIBUTES **/
  products: Product[] = [];
  categories: any[] = [
    {
      name: 'Laptops'
    },
    {
      name: 'Accessories'
    },
    {
      name: 'Cameras'
    },
  ];
  loading: boolean = false;
  productPageCounter: number = 1;
  additionalLoading: boolean = false;

  screenWidth: any;
  screenHeight: any;

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.screenWidth = window.innerWidth;
    this.screenHeight = window.innerHeight;
  }

  /** CONSTRUCTOR **/
  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  /** METHODS **/
  ngOnInit(): void {
    this.screenWidth = window.innerWidth;
    this.screenHeight = window.innerHeight;
    this.loading = true;
    setTimeout(() => {
      this.productService.getAllProducts(9, this.productPageCounter)
        .pipe(
          finalize(() => this.loading = false)
        )
        .subscribe(
          (response: any) => this.products = response,
          (error: any) => console.log(error)
        );
    }, 500);
  }

  showMoreProducts(): void {
    this.additionalLoading = true;
    this.productPageCounter = this.productPageCounter + 1;
    setTimeout(() => {
      this.productService.getAllProducts(9, this.productPageCounter)
        .pipe(
          finalize(() => this.additionalLoading = false)
        )
        .subscribe(
          (response: any) => this.products = [...this.products, ...response],
          (error: any) => console.log(error)
        );
    }, 500);
  }
}
