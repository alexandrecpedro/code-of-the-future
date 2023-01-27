import { Component } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Client } from 'src/app/interfaces/client.interface';
import { ClientObserverService } from 'src/app/services/client/client-observer.service';
import { ClientService } from 'src/app/services/client/client.service';

import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-form-dialog',
  templateUrl: './form-dialog.component.html',
  styleUrls: ['./form-dialog.component.css']
})
export class FormDialogComponent {

  constructor(
    private http:HttpClient,
    private clientObserver: ClientObserverService,
    public dialogRef: MatDialogRef<FormDialogComponent>,
    ){}

  ngOnInit(): void {
    this.clientService = new ClientService(this.http)
    this.getClients();
  }

  private clientService:ClientService = {} as ClientService;
  public clients: Client[] | undefined = [];
  public client:Client= {} as Client;

  private async getClients(){
    this.clients = await this.clientService.getClient();
  }

  create(){
    this.clientService.createClient({
      id: 0,
      name: this.client.name,
      bairro: this.client.bairro,
      cep: this.client.cep,
      cidade: this.client.cidade,
      complemento: this.client.complemento,
      cpf: this.client.cpf,
      email: this.client.email,
      estado: this.client.estado,
      phone: this.client.phone,
      logradouro: this.client.logradouro,
      numero: this.client.numero
    });
    this.getClients();
    this.clientObserver.updateQty();
    location.reload();
  }

  async save(){
    if(this.client.id && this.client.id != 0){
        const update = await this.clientService.updateClient(this.client);
        console.log(update);
    }
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  faXmark = faXmark;
}
