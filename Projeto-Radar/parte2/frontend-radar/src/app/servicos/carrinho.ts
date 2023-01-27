import { PedidoProdutoServico } from "./pedidoProdutoServico";
import { PedidoServico } from "./pedidoServico";
import { PedidoProduto } from "../models/pedidoProduto";
import { HttpClient } from "@angular/common/http";
import { Pedido } from "../models/pedido";
import { Produto } from "../models/produto";
import { ProdutoServico } from "./produtoServico";

export class Carrinho{

    private static carrinho: (PedidoProduto)[]=[];
    private static id:number=0;
    private static pedido: Pedido={"id":2,"cliente_id":3,"valor_total":40,"data":new Date((new Date()).getTime()),"cliente":{nome:"jorge"}};

    
    public static buscaTamanho():number{
        return Carrinho.carrinho.length;
    }

    public static setCliente_Id(id:Number):void{
        this.pedido.cliente_id=id;
    }

    public static listar():(PedidoProduto)[]{
        return Carrinho.carrinho;
    }

    public static setPedido(cliente_id:Number){
        Carrinho.pedido.cliente_id=cliente_id;
    }

    public static adicionaPedidoProduto(produto:Produto):void{
        let existePedidoProduto = Carrinho.verifica(produto.id);
        
        if(existePedidoProduto > -1){
            if(Number(Carrinho.carrinho[existePedidoProduto].quantidade) < Number(produto.qtd_estoque))
                Carrinho.carrinho[existePedidoProduto].quantidade = Number(Carrinho.carrinho[existePedidoProduto].quantidade) + 1;
        }else{
            let pedidoProduto={} as PedidoProduto;
            Carrinho.id++;
            pedidoProduto.quantidade=1
            pedidoProduto.id=Carrinho.id;
            pedidoProduto.produto_id=produto.id;
            pedidoProduto.valor=produto.valor;
            Carrinho.carrinho.push(pedidoProduto);
        }
     }

    public static verifica(produtoId : Number) : number{
        for (let i = 0; i < Carrinho.carrinho.length; i++) {
            const item = Carrinho.carrinho[i];
            if(item.produto_id.toString() === produtoId.toString()){
                 return i
            }
        }
        return -1
    }

    public static excluirProduto(id:number):void{
        console.log(Carrinho.carrinho);
        let item=Carrinho.carrinho.splice(id,1);
        }

    public static getValor_Total():Number{
        let total=0;
        Carrinho.carrinho.forEach(item=>{
            total+=Number(item.quantidade)*Number(item.valor);
        })
        return Carrinho.pedido.valor_total=new Number(total);
    }

    public static async salvar(http:HttpClient):Promise<void>{
        let id =new Number(Number((await new PedidoServico(http).getLast())?.id)+1);
        !id? 0:Carrinho.pedido.id=id;

        Carrinho.carrinho.forEach(async item=>{
            item.id=new Number(Carrinho.pedido?.id.toString()+item.id)
            console.log("teste "+item.id)
            item.pedido_id=Carrinho.pedido.id;
            return await new PedidoProdutoServico(http).criar(item)
        })
        await new PedidoServico(http).criar(Carrinho.pedido);
    }

    public static reset(){
        while(this.carrinho.length>0){
            this.carrinho.pop();
        }
    }
}