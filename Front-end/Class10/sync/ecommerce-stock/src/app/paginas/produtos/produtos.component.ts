import { Produto } from './../../models/produto';
import { Component, OnInit } from '@angular/core';
import { PedidosServicoService } from 'src/app/servicos/pedidos-servico.service';
import { CarrinhoComponent } from '../carrinho/carrinho.component';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {

  public produtos = [
    {
      id: 0,
      nome: "aaa",
      descricao: "aaa",
      preco: "10"
   },
   {
    id: 1,
    nome: "bbb",
    descricao: "bbb",
    preco: "100"
  },
  {
    id: 2,
    nome: "ccc",
    descricao: "ccc",
    preco: "1000"
  },
  ]

  idPedido = 0

  constructor(
    private pedido: PedidosServicoService
    ) { }

  ngOnInit(): void {
  }

  checaProduto(id:number){
   let verificaPedido:Produto = {} as Produto

    for (let i = 0; i < this.produtos.length; i++) {

      if(id == this.produtos[i].id){

        verificaPedido = this.produtos[i]
        this.pedido.getPedido(verificaPedido)
        // this.idPedido = this.idPedido + 1
        // this.pedido.getPedido(this.idPedido)
        return
      }
    }

  }
}
