import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientService } from '../client/client.service';

@Injectable({
  providedIn: 'root'
})
export class ObserverClientService {
  /** ATTRIBUTES **/
  quantity: Number = 0;

  /** CONSTRUCTOR **/
  constructor(private http: HttpClient) {
    this.updateQuantity();
  }

  /** METHODS **/
  async updateQuantity() {
    console.log("Entered the method");
    let list = await new ClientService(this.http).listClients();
    this.quantity = list ? list.length : 0;
  }
}
