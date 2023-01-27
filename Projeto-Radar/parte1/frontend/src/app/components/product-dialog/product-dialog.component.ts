import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { OrderObserverService } from 'src/app/services/order/order-observer.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.css']
})
export class ProductDialogComponent implements OnInit{

  constructor(
    private http:HttpClient, 
    private productService: ProductService,
    private orderObserver: OrderObserverService
  ){}
  
  products: Product[] | undefined= [];
  orderedProducts: Product[] | undefined = [];
  product: Product = {} as Product;
  pages: number[] = [];
  start:number = 0;
  end:number = 8;

  ngOnInit():void{
    this.productService = new ProductService(this.http);
    this.getProducts();
    
  }
  private async getProducts(){
    this.products = await this.productService.getProduct();
    this.getPageNumber();
  }

  public getPageNumber(){
    if(this.products){
      let npages = Math.ceil(this.products.length / 8);
      for(let i=1; i<=npages;i++){
        this.pages.push(i);
      }
    }
  }

  nextPage(){
    if(this.products && this.end >= this.products.length) return;
    this.start += 8;
    this.end +=8;
  }

  previousPage(){
    if(this.start<=0) return;
    this.start -= 8;
    this.end -=8;
  }


  selectProduct(product: Product){
    this.orderObserver.setProducts(product);
  }
}
