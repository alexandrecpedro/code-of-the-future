import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Cliente } from "../models/cliente";

import { firstValueFrom } from 'rxjs';

export class ClienteServico{

    constructor(private http:HttpClient) { }

    public async lista(): Promise<Cliente[] | undefined> {
        let clientes:Cliente[] | undefined = await firstValueFrom(this.http.get<Cliente[]>(`${environment.api}/clientes`))
        return clientes;
    }

    public async getLast(): Promise<Cliente | undefined> {
        let cliente:Cliente[] | undefined = await firstValueFrom(this.http.get<Cliente[]>(`${environment.api}/clientesLast`))
        return cliente.at(0);
    }

    public async criar(cliente:Cliente): Promise<Cliente | undefined> {
        let clienteRest:Cliente | undefined = await firstValueFrom(this.http.post<Cliente>(`${environment.api}/clientes/`, cliente))
        return clienteRest;
    }

    public async update(cliente:Cliente): Promise<Cliente | undefined> {
        let clienteRest:Cliente | undefined = await firstValueFrom(this.http.put<Cliente>(`${environment.api}/clientes/${cliente.id}`, cliente))
        return clienteRest;
    }

    public async buscaPorId(id:Number): Promise<Cliente | undefined> {
        return await firstValueFrom(this.http.get<Cliente | undefined>(`${environment.api}/clientes/${id}`))
    }

    public excluirPorId(id:Number) {
        firstValueFrom(this.http.delete(`${environment.api}/clientes/${id}`))
    }
}