import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Pedido } from "../models/pedido";

import { firstValueFrom } from 'rxjs';

export class PedidoServico{

    constructor(private http:HttpClient) { }

    public async lista(): Promise<Pedido[] | undefined> {
        let pedidos:Pedido[] | undefined = await firstValueFrom(this.http.get<Pedido[]>(`${environment.api}/pedidos`))
        return pedidos;
    }

    public async listaContrario(): Promise<Pedido[] | undefined> {
        let pedidos:Pedido[] | undefined = await firstValueFrom(this.http.get<Pedido[]>(`${environment.api}/pedidosLastList`))
        return pedidos;
    }

    public async criar(pedido:Pedido): Promise<Pedido | undefined> {
        let pedidoRest:Pedido | undefined = await firstValueFrom(this.http.post<Pedido>(`${environment.api}/pedidos`, pedido))
        return pedidoRest;
    }

    public async update(pedido:Pedido): Promise<Pedido | undefined> {
        let pedidoRest:Pedido | undefined = await firstValueFrom(this.http.put<Pedido>(`${environment.api}/pedidos/${pedido.id}`, pedido))
        return pedidoRest;
    }

    public async buscaPorId(id:Number): Promise<Pedido | undefined> {
        return await firstValueFrom(this.http.get<Pedido | undefined>(`${environment.api}/pedidos/${id}`))
    }

    public async getLast(): Promise<Pedido | undefined> {
        let pedido:Pedido | undefined = await firstValueFrom(this.http.get<Pedido>(`${environment.api}/pedidosLast`))
        return pedido;
    }

    public excluirPorId(id:Number) {
        firstValueFrom(this.http.delete(`${environment.api}/pedidos/${id}`))
    }

    public async buscaPorClienteId(cliente_id:Number): Promise<Pedido[] | undefined> {
        let pedidos:Pedido[] | undefined = await firstValueFrom(this.http.get<Pedido[]>(`${environment.api}/pedidos`))
        return pedidos.filter(pedido => cliente_id==pedido.cliente_id);
    }

    public async buscaPorProdutoId(produto_id:Number): Promise<Pedido | undefined> {
        return await firstValueFrom(this.http.get<Pedido | undefined>(`${environment.api}/pedidos/produtos/${produto_id}`));
    }
}