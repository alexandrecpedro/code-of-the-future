import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Categoria } from "../models/categoria";

import { firstValueFrom } from 'rxjs';

export class CategoriaServico{

    constructor(private http:HttpClient) { }

    public async lista(): Promise<Categoria[] | undefined> {
        let categorias:Categoria[] | undefined = await firstValueFrom(this.http.get<Categoria[]>(`${environment.api}/categorias`))
        return categorias;
    }

    public async criar(categoria:Categoria): Promise<Categoria | undefined> {
        let categoriaRest:Categoria | undefined = await firstValueFrom(this.http.post<Categoria>(`${environment.api}/categorias/`, categoria))
        return categoriaRest;
    }

    public async update(categoria:Categoria): Promise<Categoria | undefined> {
        let categoriaRest:Categoria | undefined = await firstValueFrom(this.http.put<Categoria>(`${environment.api}/categorias/${categoria.id}`, categoria))
        return categoriaRest;
    }

    public async buscaPorId(id:Number): Promise<Categoria | undefined> {
        return await firstValueFrom(this.http.get<Categoria | undefined>(`${environment.api}/categorias/${id}`))
    }

    public excluirPorId(id:Number) {
        firstValueFrom(this.http.delete(`${environment.api}/categorias/${id}`))
    }

}