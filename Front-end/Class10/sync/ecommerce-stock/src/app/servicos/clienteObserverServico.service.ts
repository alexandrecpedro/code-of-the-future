import { Injectable } from '@angular/core';
import { ClienteServico } from './clienteServico';

@Injectable({
  providedIn: 'root'
})
export class ClienteObserverServicoService {

  constructor() { 
    this.atualizaQuantidade()
  }

  public quantidade:Number = 0

  atualizaQuantidade(){
    console.log("--- Entrou no metodo ---")
    this.quantidade = ClienteServico.buscaClientes().length;
  }
}
