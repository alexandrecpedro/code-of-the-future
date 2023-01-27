import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class ProductObserverService {

  constructor(private http: HttpClient) { 
    this.updateQty();
  }

  public productQty: Number = 0;

  async updateQty(){
    let list = await new ProductService(this.http).getProduct();
    this.productQty = list ? list.length : 0;
  }
}
