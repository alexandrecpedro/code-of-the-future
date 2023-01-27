import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientService } from './client.service';

@Injectable({
  providedIn: 'root'
})
export class ClientObserverService {

  constructor(private http: HttpClient) {
    this.updateQty();
   
 }

public clientQty: Number = 0;

async updateQty(){
  let list = await new ClientService(this.http).getClient();
  this.clientQty = list ? list.length : 0;
}
}
