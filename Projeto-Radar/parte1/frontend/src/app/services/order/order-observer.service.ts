import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from 'src/app/interfaces/client.interface';
import { Product } from 'src/app/interfaces/product.interface';
import { OrderService } from './order.service';

@Injectable({
  providedIn: 'root'
})
export class OrderObserverService {

  constructor(private http: HttpClient) { 
    this.updateQty();
   }

   public orderQty: Number = 0;
   public profileMock: string | undefined ;

   public productsOrdered: {product: Product, qty: number}[] = [];
   public orderClient: Client = {} as Client;

   async updateQty(){
    let list = await new OrderService(this.http).getOrder();
    this.orderQty = list ? list.length : 0;
  }

  updateStock(index: number, newStock: number){
    this.productsOrdered[index].product.stockQty = Number(this.productsOrdered[index].product.stockQty) + newStock;
  }
  delete(newOrderedProducts:  {product: Product, qty: number}[]){
    this.productsOrdered = newOrderedProducts;
  }

  updateProductOrdered(index: number, qty: number){
    this.productsOrdered[index].qty=qty;

  }

 public setProducts(product: Product){
    this.productsOrdered.push({product, qty: 1});
    
  }

  public setClient(client: Client){
    this.orderClient = client;
    this.profileMock = this.orderClient.name.charAt(0);
  }

  public sumValue(): number{
    let sum:number = 0;
    this.productsOrdered.map(element=>{
      sum = sum + (Number(element.product.value) * Number(element.qty));

    });
    return sum;
  }

  
}
