import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Client } from 'src/app/models/client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  /** ATTRIBUTES **/
  // private static clients: Client[] = [];

  /** CONSTRUCTOR **/
  constructor(private http: HttpClient) { }

  /** METHODS **/
  async listClients(): Promise<Client[] | undefined> {
    let clients: Client[] | undefined = await firstValueFrom(this.http.get<Client[]>(`${environment.api}/clients`));
    return clients;
  }

  async findById(id: Number): Promise<Client | undefined> {
    return await firstValueFrom(this.http.get<Client | undefined>(`${environment.api}/clients/${id}`));
  }

  async create(client: Client): Promise<Client | undefined> {
    let clientRest: Client | undefined = await firstValueFrom(this.http.post<Client>(`${environment.api}/clients/`, client));
    return clientRest;
  }

  async update(client: Client): Promise<Client | undefined> {
    let clientRest: Client | undefined = await firstValueFrom(this.http.put<Client>(`${environment.api}/clients/${client.id}`, client));
    return clientRest;
  }

  deleteById(id: Number): void {
    firstValueFrom(this.http.delete(`${environment.api}/clients/${id}`));
  }
}
