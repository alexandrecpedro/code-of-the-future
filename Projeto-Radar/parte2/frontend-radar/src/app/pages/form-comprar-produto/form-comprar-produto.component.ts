import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Categoria } from 'src/app/models/categoria';
import { Pedido } from 'src/app/models/pedido';
import { PedidoProduto } from 'src/app/models/pedidoProduto';
import { Produto } from 'src/app/models/produto';
import { CategoriaServico } from 'src/app/servicos/categoriaServico';
import { ClienteServico } from 'src/app/servicos/clienteServico';
import { PedidoProdutoServico } from 'src/app/servicos/pedidoProdutoServico';
import { PedidoServico } from 'src/app/servicos/pedidoServico';
import { ProdutoServico } from 'src/app/servicos/produtoServico';

@Component({
  selector: 'app-form-comprar-produto',
  templateUrl: './form-comprar-produto.component.html',
  styleUrls: ['./form-comprar-produto.component.css']
})
export class FormComprarProdutoComponent implements OnInit {

  public categorias:Categoria[]=[];
  public selecionado:boolean[]=[]
  public categoria_id: Number=-1;
  public categoriaSelecionado:String ="";
  public qtd: Number = 0;

  constructor(
    private router:Router,
    private http:HttpClient,
    private routerParams:ActivatedRoute 
  ) { }

  private produtoServico:ProdutoServico = {} as ProdutoServico;
  private clienteServico:ClienteServico = {} as ClienteServico;
  public produto:Produto | undefined = {} as Produto;
  public pedidoProdutoServico: PedidoProdutoServico = {} as PedidoProdutoServico; 
  public pedidoServico: PedidoServico = {} as PedidoServico; 

  ngOnInit(): void {
    this.produtoServico = new ProdutoServico(this.http);
    this.pedidoServico = new PedidoServico(this.http);
    this.pedidoProdutoServico = new PedidoProdutoServico(this.http);
    this.clienteServico = new ClienteServico(this.http);
    let id:Number = this.routerParams.snapshot.params['id'];
    if(id){
      this.carregaProduto(id);
    }
    this.listaCategorias();
  }

  async registrar(){
    let produto_id: Number = 0
    let qtd_estoque:Number = 0;
    let custo:Number = 0;
    let pedido = {} as Pedido;
    
    pedido.cliente_id = 3;
    
    if(this.produto?.id) produto_id = this.produto.id;
    if(this.qtd) qtd_estoque = this.qtd;
    if(this.produto?.custo) custo = this.produto.custo;

    
    pedido.valor_total =  - Number(qtd_estoque) * Number(custo);
    console.log("Caiu aq");
    await this.pedidoServico.criar(pedido);
    console.log("agr aq");
    let pedido_id = (await this.pedidoServico.getLast())?.id;
    console.log("e aq??????")

    if(pedido_id){
      let pedidoProduto = {} as PedidoProduto;
      pedidoProduto.pedido_id = pedido_id;
      pedidoProduto.produto_id = produto_id;
      pedidoProduto.valor = -custo;
      pedidoProduto.quantidade = -qtd_estoque;
      await this.pedidoProdutoServico.criar(pedidoProduto);
    }
    this.router.navigateByUrl("/produtos");
  }

  async verificaUndefined(){
    let produto_id: Number = 0;
    let qtd_estoque:Number = 0;
    let custo:Number = 0;
    let pedido = {} as Pedido;
    
    pedido.cliente_id = 0;
    
    if(this.produto?.id) produto_id = this.produto.id;
    if(this.qtd) qtd_estoque = this.qtd;
    if(this.produto?.custo) custo = this.produto.custo;

    
    pedido.valor_total =  - Number(qtd_estoque) * Number(custo);
    await this.pedidoServico.criar(pedido);
    let pedido_id = (await this.pedidoServico.getLast())?.id;

    if(pedido_id){
      let pedidoProduto = {} as PedidoProduto;
      pedidoProduto.id = new Number (pedido_id+ "01");
      pedidoProduto.pedido_id = pedido_id;
      pedidoProduto.produto_id = produto_id;
      pedidoProduto.valor = -custo;
      pedidoProduto.quantidade = - qtd_estoque;
      await this.pedidoProdutoServico.criar(pedidoProduto);
    }
  }

  private async listaCategorias(){
    let categorias=await new CategoriaServico(this.http).lista();
    categorias?.forEach(categoria => {
      if(this.produto?.categoria_id&&categoria.id.toString()===this.produto?.categoria_id.toString()){
        this.selecionado.push(true)
        
      }else{
        this.selecionado.push(false)
      }
      this.categorias.push(categoria);
    });
  }

  private async carregaProduto(id:Number){
    this.produto = await this.produtoServico.buscaPorId(id);
    if(this.produto?.id)this.categoria_id=this.produto.id;
    let categoria_id=this.produto?.categoria_id;
    if(categoria_id)this.categoria_id=categoria_id
  }

  private atualizaQuantidade(novaQtd : Number) : Number{
    let qtdAtual = Number(this.produto?.qtd_estoque);
    return Number(novaQtd) + qtdAtual
  }
}
