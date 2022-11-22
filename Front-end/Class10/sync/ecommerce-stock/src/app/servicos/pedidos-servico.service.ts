import { Produto } from './../models/produto';
import { Injectable, Input, Output } from '@angular/core';


// interface Carrinhos{
//     idPedido: number,
//     id: number,
//     nome: String,
//     descricao: String,
//     preco: string
// }

@Injectable({
  providedIn: 'root'
})
export class PedidosServicoService {

  carrinho:any[] = []
  idKey = 0

  constructor() { }

  getPedido(pedido:Produto){

    this.carrinho.push({...pedido, idPedido: this.idKey})
    this.idKey++

    // this.carrinho.map((res, i)=>{ this.idKey = i})
    // let newCarrinho:any = []

    // this.carrinho = newCarrinho.concat(pedido,{newIdkey: this.idKey})
    // this.idKey++

  //   this.carrinho.forEach((obj, i) => {
  //   Object.assign(obj,{idKeys: this.idKey})
  //   this.idKey++
  // })
  // Object.assign(this.carrinho,{idKeys: this.idKey})
  // this.idKey++
    // console.log(this.carrinho)
  }

}

