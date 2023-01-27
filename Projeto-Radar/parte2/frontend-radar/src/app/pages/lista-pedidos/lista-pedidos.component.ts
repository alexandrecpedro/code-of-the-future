import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pedido } from 'src/app/models/pedido';
import { ClienteServico } from 'src/app/servicos/clienteServico';
import { PedidoServico } from 'src/app/servicos/pedidoServico';

@Component({
  selector: 'app-lista-pedidos',
  templateUrl: './lista-pedidos.component.html',
  styleUrls: ['./lista-pedidos.component.css']
})
export class ListaPedidosComponent implements OnInit {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { }

  public pedidosServico: PedidoServico = {} as PedidoServico;
  public clienteServico: ClienteServico = {} as ClienteServico;
  public pedidos: Pedido[] | undefined = [];
  public nomeCliente: String[] = [];

  ngOnInit(): void {
    this.pedidosServico = new PedidoServico(this.http);
    this.clienteServico = new ClienteServico(this.http);
    this.listaDePedidos();
  }
  

  private async listaDePedidos(){
    this.pedidos = await this.pedidosServico.listaContrario();
  }
  number (a : Number){
    return Number(a)
  }

  somaTotal(){
    let valorTotal = 0;
    this.pedidos?.slice(0, 5).forEach(index => {
      valorTotal += Number(index.valor_total);
    })
    return Number(valorTotal)
  }
}
