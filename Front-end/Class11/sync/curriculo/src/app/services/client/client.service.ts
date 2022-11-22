import { Injectable } from '@angular/core';
import { Client } from 'src/app/models/client';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private static clients: Client[] = [];

  constructor() { }


  /** METHODS **/
  static searchClients(): Client[] {
    return ClientService.clients;
  }

  static searchClientById(id: Number): Client {
    let client: Client = {} as Client;
    let findClient = ClientService.clients.find(client => client.id === id);
    if (findClient !== undefined) client = findClient;
    return client;
  }

  static addClient(client: Client): void {
    client.id = ClientService.searchClients().length + 1;
    ClientService.clients.push(client);
  }

  static updateClient(client: Client): void {
    for (let i=0; i<ClientService.clients.length; i++) {
      let clientDb = ClientService.clients[i];
      if (clientDb.id === client.id) {
        clientDb = {...client};
        break;
      }
    }
  }

  static deleteClient(client: Client): void {
    let newList = [];
    for (let i=0; i<ClientService.clients.length; i++) {
      let clientDb = ClientService.clients[i];
      if (clientDb.id !== client.id) {
        newList.push(clientDb);
      }
    }

    ClientService.clients = newList;
  }
}
