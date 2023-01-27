import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PedidoProduto } from "../models/pedidoProduto";

import { firstValueFrom } from 'rxjs';
import { ProdutoServico } from './produtoServico';

export class PedidoProdutoServico{

    constructor(private http:HttpClient) { }

    public async lista(): Promise<PedidoProduto[] | undefined> {
        let pedidosProdutos:PedidoProduto[] | undefined = await firstValueFrom(this.http.get<PedidoProduto[]>(`${environment.api}/pedidosProdutos`))
        return pedidosProdutos;
    }

    public async criar(pedidoProduto:PedidoProduto): Promise<PedidoProduto | undefined> {
        let pedidoProdutoRest:PedidoProduto | undefined = await firstValueFrom(this.http.post<PedidoProduto>(`${environment.api}/pedidosProdutos`, pedidoProduto))
        let produto = await new ProdutoServico(this.http).buscaPorId(pedidoProduto.produto_id);
        let qtd = pedidoProduto.quantidade
        if(pedidoProduto.valor>0){
            qtd =new Number(Number(qtd))
        }
        console.log(produto)
        if(produto?.qtd_estoque) produto.qtd_estoque=new Number(Number(produto?.qtd_estoque)-Number(qtd))
        
        if(produto) await new ProdutoServico(this.http).update(produto)
        return pedidoProdutoRest;
    }

    public async update(pedidoProduto:PedidoProduto): Promise<PedidoProduto | undefined> {
        let pedidoProdutoRest:PedidoProduto | undefined = await firstValueFrom(this.http.put<PedidoProduto>(`${environment.api}/pedidosProdutos/${pedidoProduto.id}`, pedidoProduto))
        return pedidoProdutoRest;
    }

    public async buscaPorId(id:Number): Promise<PedidoProduto | undefined> {
        return await firstValueFrom(this.http.get<PedidoProduto | undefined>(`${environment.api}/pedidosProdutos/${id}`))
    }

    public async getLast(): Promise<PedidoProduto | undefined> {
        let pedidoProduto:PedidoProduto[] | undefined = await firstValueFrom(this.http.get<PedidoProduto[]>(`${environment.api}/pedidosProdutosLast`))
        return pedidoProduto.at(0);
    }

    public excluirPorId(id:Number) {
        firstValueFrom(this.http.delete(`${environment.api}/pedidosProdutos/${id}`))
    }
}