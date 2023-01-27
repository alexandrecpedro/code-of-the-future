import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente';
import { PedidoProduto } from 'src/app/models/pedidoProduto';
import { Carrinho } from 'src/app/servicos/carrinho';
import { ClienteServico } from 'src/app/servicos/clienteServico';
import { PedidoProdutoServico } from 'src/app/servicos/pedidoProdutoServico';
import { ProdutoServico } from 'src/app/servicos/produtoServico';

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent implements OnInit {

  public items:  (PedidoProduto)[]=[];
  public nomes:String[]=[]
  public clientes: (Cliente)[]=[];
  public produtoServico:ProdutoServico= {} as ProdutoServico;
  public pedidoProdutoServico:PedidoProdutoServico= {} as PedidoProdutoServico;
  public clienteServico:ClienteServico= {} as ClienteServico;
  public valor_total:Number=0;
  public cliente: String | undefined="1";
  constructor(
    private http:HttpClient,
    private router:Router,
  ) { }
  
  ngOnInit(): void {
    this.produtoServico = new ProdutoServico(this.http);
    this.pedidoProdutoServico = new PedidoProdutoServico(this.http);
    this.clienteServico = new ClienteServico(this.http);
    this.listaItems();
    this.calcularValorTotal()
  }

  private async listaItems() {
    this.items = Carrinho.listar();
    let clientes= await this.clienteServico.lista();
    if(!clientes){
        
    }else{
      this.clientes= clientes
    }
    this.items?.forEach(async item =>{
      let nome = await this.produtoServico.buscaPorId(item.produto_id);
      if(!nome){}else{
        this.nomes.push(nome.nome);
      }
    });
  }

  public async salvar() {
    Carrinho.setCliente_Id(this.clientes[Number(this.cliente?.split("-")[0].split(" ")[0])-1].id);
    await Carrinho.salvar(this.http);
    Carrinho.reset();
  }

  public convert(id:Number):number{
    console.log(id);
    return Number(id);
  }

  Excluir(id:number){
    Carrinho.excluirProduto(id);
    this.calcularValorTotal();
  }

  multiplicacao(a:Number,b:Number):number{
    return Number(a)*Number(b)
  }

  calcularValorTotal() {
    this.valor_total=Carrinho.getValor_Total();
  }
  
  async Subtrair(id:number){
    let itemE:PedidoProduto={} as PedidoProduto;
    for (let i = 0; i < this.items.length; i++) {
      if(i==id){
        itemE = this.items[i];
      }
    }
    let qtd=itemE.quantidade;
    let quantidade=0;
    if(qtd){
      quantidade=Number(qtd);
    }
    if((quantidade--)>1){
      this.items?.forEach(item=>{
        if(item.id===itemE.id){
          item.quantidade=new Number(quantidade--)
        }
      })
      this.calcularValorTotal();
    }else{
      this.Excluir(id)
    }
  }
  async Adicionar(id:number){
    let itemE:PedidoProduto={} as PedidoProduto;
    for (let i = 0; i < this.items.length; i++) {
      if(i==id){
        itemE = this.items[i];
      }
    }
    let limiteSuperior=0;
    let limite= (await new ProdutoServico(this.http).buscaPorId(itemE.produto_id))?.qtd_estoque
    if(limite){
      limiteSuperior=Number(limite);
    }
    let qtd=itemE.quantidade;
    let quantidade=0;
    if(qtd){
      quantidade=Number(qtd);
    }
    if(limiteSuperior>quantidade++){
      console.log(limiteSuperior)
      this.items?.forEach(item=>{
        if(item.id===itemE.id){
          item.quantidade=new Number(quantidade++);
        }
      })
      this.calcularValorTotal();
    }
  }
  number (a : Number){
    return Number(a)
  }
}


