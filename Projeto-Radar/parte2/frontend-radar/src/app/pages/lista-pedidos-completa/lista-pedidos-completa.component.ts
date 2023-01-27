import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pedido } from 'src/app/models/pedido';
import { ClienteServico } from 'src/app/servicos/clienteServico';
import { PedidoServico } from 'src/app/servicos/pedidoServico';

@Component({
  selector: 'app-lista-pedidos',
  templateUrl: './lista-pedidos-completa.component.html',
  styleUrls: ['./lista-pedidos-completa.component.css']
})
export class ListaPedidosCompletaComponent implements OnInit {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { }

  public pedidosServico: PedidoServico = {} as PedidoServico;
  public pedidos: Pedido[] | undefined = [];
  public clienteServico: ClienteServico = {} as ClienteServico;
  public nomeClienteArray: String[] = [];
  public dict: Map<Number,number> = new Map();

  ngOnInit(): void {
    this.pedidosServico = new PedidoServico(this.http);
    this.clienteServico = new ClienteServico(this.http);
    this.listaDePedidos();
  }
  
  public nomeCliente(id: Number|undefined){
    let cli:number|undefined=1
    console.log(id)
    if(id) cli=this.dict.get(id)
    if(cli) return this.nomeClienteArray[cli]
    return "asd"

  }

  private async listaDePedidos(){
    let clientes = await this.clienteServico.lista();
    clientes?.forEach(cliente=>{
      this.nomeClienteArray.push(cliente.nome)
      this.dict.set(cliente.id,this.nomeClienteArray.length-1)
    })
    this.pedidos = await this.pedidosServico.lista();
  }
  number (a : Number){
    return Number(a)
  }

  somaTotal(){
    let valorTotal = 0;
    this.pedidos?.forEach(index => {
      valorTotal += Number(index.valor_total);      
    })
    
    return Number(valorTotal)
  }

}
