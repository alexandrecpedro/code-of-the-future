import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Client } from 'src/app/models/client';
import { ClientService } from 'src/app/services/client/client.service';
import { ObserverClientServiceService } from 'src/app/services/observer-client-service/observer-client-service.service';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  /** ATTRIBUTES **/
  clients: Client[] = ClientService.searchClients();

  /** CONSTRUCTOR **/
  constructor(
    private router: Router,
    private observerClientService: ObserverClientServiceService
  ) { }

  /** METHODS **/
  ngOnInit(): void {
  }

  new() {
    this.router.navigate(["/form"]);
  }

  // updateClient(client: Client): void {
  //   ClientService.updateClient(client);
  //   this.router.navigate(["/form"]);
  // } 

  deleteClient(client: Client): void {
    ClientService.deleteClient(client);
    this.clients = ClientService.searchClients();
    this.observerClientService.updateQuantity();
  }

}
