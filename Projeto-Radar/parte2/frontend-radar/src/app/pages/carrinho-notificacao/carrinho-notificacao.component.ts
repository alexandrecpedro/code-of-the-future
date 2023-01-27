import { Component, OnInit } from '@angular/core';
import { Carrinho } from 'src/app/servicos/carrinho';

@Component({
  selector: 'app-carrinho-notificacao',
  templateUrl: './carrinho-notificacao.component.html',
  styleUrls: ['./carrinho-notificacao.component.css']
})
export class CarrinhoNotificacaoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  getQuantidade(){
    return Carrinho.buscaTamanho();
  }
}
