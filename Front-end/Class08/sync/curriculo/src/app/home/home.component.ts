import { Component, OnInit } from '@angular/core';
import { Client } from '../models/client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  /** ATTRIBUTES **/
  client: Client = {} as Client;

  clients: Client[] = [
    { id: 1, name: "Leandro", cpf: "33333333333", phone: 11999999999, address: "Rua teste, 123", date: new Date(), value: 33.45},
    { id: 2, name: "Marcia", cpf: "33333333233", phone: 11993999999, address: "Rua av, 123", date: new Date(), value: 4533},
    { id: 3, name: "Livinia", cpf: "33333333331", phone: 11999599999, address: "Rua florida, 123", date: new Date(), value: 1269.03}
  ]

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  ngOnInit(): void {
    // this.client = this.clients[0];
  }

  clicked(): void {
    let newClient: Client = {
      id: this.clients.length + 1,
      name: this.client.name,
      cpf: "34567898765",
      phone: 11999599999,
      address: this.client.address,
      date: new Date(),
      value: 0
    };

    this.clients.push(newClient);
  }

}
