import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Client } from 'src/app/interfaces/client.interface';
import { ClientService } from 'src/app/services/client/client.service';
import { OrderObserverService } from 'src/app/services/order/order-observer.service';

@Component({
  selector: 'app-client-dialog',
  templateUrl: './client-dialog.component.html',
  styleUrls: ['./client-dialog.component.css']
})
export class ClientDialogComponent implements OnInit{

  constructor(
    private http:HttpClient, 
    private clientService: ClientService,
    public orderObserver: OrderObserverService
  ){}

  clients: Client[] | undefined = [];
  client: Client | undefined = {} as Client;

  ngOnInit(): void {
    this.clientService = new ClientService(this.http);
    this.getClients();
  }

  async getClients(){
    this.clients = await this.clientService.getClient();
  }
  
}
