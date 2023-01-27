import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';
import { PosicoesProduto } from '../models/PosicoesProduto';


@Injectable({
  providedIn: 'root'
})
export class PosicoesProdutoService {

  constructor(private http:HttpClient) { }

  public async lista(): Promise<PosicoesProduto[] | undefined> {
      let posicoesProdutos:PosicoesProduto[] | undefined = await firstValueFrom(this.http.get<PosicoesProduto[]>(`${environment.api}/posicoesProdutos`))
      return posicoesProdutos;
  }

  public async listaComProduto(id:Number): Promise<PosicoesProduto[] | undefined> {
    let posicoesProdutos:PosicoesProduto[] | undefined = await firstValueFrom(this.http.get<PosicoesProduto[]>(`${environment.api}/listaPorId/${id}`))
    return posicoesProdutos;
}


public async buscaPorId(id:Number): Promise<PosicoesProduto | undefined> {
    return await firstValueFrom(this.http.get<PosicoesProduto | undefined>(`${environment.api}/posicoesProdutos/${id}`))
}
  public async criar(posicoesProduto:PosicoesProduto): Promise<PosicoesProduto | undefined> {
      let posicoesProdutoRest:PosicoesProduto | undefined = await firstValueFrom(this.http.post<PosicoesProduto>(`${environment.api}/posicoesProdutos/`, posicoesProduto))
      return posicoesProdutoRest;
  }

  public async update(posicoesProduto:PosicoesProduto): Promise<PosicoesProduto | undefined> {
      let posicoesProdutoRest:PosicoesProduto | undefined = await firstValueFrom(this.http.put<PosicoesProduto>(`${environment.api}/posicoesProdutos/${posicoesProduto.id}`, posicoesProduto))
      return posicoesProdutoRest;
  }


  public excluirPorId(id:Number) {
      firstValueFrom(this.http.delete(`${environment.api}/posicoesProdutos/${id}`))
  }

}
