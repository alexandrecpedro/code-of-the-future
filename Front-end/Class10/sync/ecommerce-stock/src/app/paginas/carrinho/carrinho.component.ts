import { Produto } from './../../models/produto';
import { PedidosServicoService } from '../../servicos/pedidos-servico.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent implements OnInit {

  constructor( private pedidos: PedidosServicoService) { }

  mostraPedido: Produto[] = this.pedidos.carrinho

  total: number = 0

  ngOnInit(): void {
    this.totalCompra()
  }
  // adicionaIdPedido(){
  //   this.mostraPedido.map((obj, i) => {
  //     this.idKeyPedido = i
  //   })
  // }

  deletaPedido(id:number|undefined){
    if(id || id == 0){
      for (let i = 0; i <= this.mostraPedido.length; i++) {
        if(id == this.mostraPedido[i].idPedido){
          let resultado = this.mostraPedido.map(res => res.idPedido).indexOf(id)
          this.total -= Number(this.mostraPedido[resultado].preco)
          this.mostraPedido.splice(resultado,1)
          return
        }
      }
    }

    // this.mostraPedido.splice(deletaId ,1)
    // this.total =- Number(this.mostraPedido[deletaId].preco)
    // console.log(this.mostraPedido)
  }

  totalCompra(){
    for (let i = 0; i < this.mostraPedido.length; i++) {
      this.total += Number(this.mostraPedido[i].preco)
    }
  }

}
