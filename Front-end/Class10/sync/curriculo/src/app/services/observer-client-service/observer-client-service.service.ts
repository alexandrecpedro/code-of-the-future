import { Injectable } from '@angular/core';
import { ClientService } from '../client/client.service';

@Injectable({
  providedIn: 'root'
})
export class ObserverClientService {
  /** ATTRIBUTES **/
  quantity: Number = 0;

  /** CONSTRUCTOR **/
  constructor() {
    this.updateQuantity();
  }

  /** METHODS **/
  updateQuantity() {
    console.log("Entered the method");
    this.quantity = ClientService.searchClients().length;
  }
}
