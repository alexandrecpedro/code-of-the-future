import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize, map } from 'rxjs';
import SwiperCore, { A11y, Autoplay, Controller, Navigation, Pagination, Scrollbar, Thumbs, Virtual, Zoom } from 'swiper';
import { CartService } from '../services/cart/cart.service';
import { ProductService } from '../services/product/product.service';

// install Swiper components
SwiperCore.use([
  Navigation,
  Pagination,
  Scrollbar,
  A11y,
  Virtual,
  Zoom,
  Autoplay,
  Thumbs,
  Controller,
]);

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  /** ATTRIBUTES **/
  id!: number;
  product: any;
  quantity!: number;
  showcaseImages: string[] = [];
  loading: boolean = false;
  
  /** CONSTRUCTOR **/
  constructor(
    private _cart: CartService,
    private _product: ProductService,
    private _route: ActivatedRoute
  ) {}

  /** METHODS **/
  ngOnInit(): void {
      this.loading = true;
      this._route.paramMap
        .pipe(
          map((param: any) => { return param.params.id }),
          finalize(() => this.loading = false)
        )
        .subscribe((productId: string) => {
          this.id = parseInt(productId);
          this._product.getSingleProduct(this.id).subscribe((product) => {
            this.product = product;
            if (product.quantity === 0) this.quantity = 0;
            else this.quantity = 1;

            if (product.images) this.showcaseImages = product.images.split(';');
            // this.loading = false
          })
        })
  }

  addToCart(): void {
    this._cart.addProduct({
      id: this.id,
      price: this.product.price,
      quantity: this.quantity,
      image: this.product.image,
      title: this.product.title,
      maxQuantity: this.product.quantity,
    })
  }
}
