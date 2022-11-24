import { HttpClient } from '@angular/common/http';
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
  clients: Client[] | undefined = [];
  private clientService: ClientService = {} as ClientService;

  /** CONSTRUCTOR **/
  constructor(
    private http: HttpClient,
    private observerClientService: ObserverClientServiceService,
    private router: Router
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.clientService = new ClientService(this.http);
    this.listClients();
  }

  private async listClients() {
    this.clients = await this.clientService.listClients();
  }

  new() {
    this.router.navigateByUrl("/form");
  }

  // updateClient(client: Client): void {
  //   ClientService.updateClient(client);
  //   this.router.navigate(["/form"]);
  // } 

  async deleteClient(client: Client): Promise<void> {
    if (confirm("Confirm ?")) {
      await this.clientService.deleteById(client.id);
      this.clients = await this.clientService.listClients();
      this.observerClientService.updateQuantity();
    }
  }
}
