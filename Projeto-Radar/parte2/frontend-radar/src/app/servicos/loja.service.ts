import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Loja } from '../models/loja';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LojaService {

  constructor(private http: HttpClient) { }

  public async lista(): Promise<Loja[] | undefined> {
    let lojas:Loja[] | undefined = await firstValueFrom(this.http.get<Loja[]>(`${environment.api}/lojas`))
    return lojas;
}
public async criar(loja:Loja): Promise<Loja | undefined> {
  let lojaRest:Loja | undefined = await firstValueFrom(this.http.post<Loja>(`${environment.api}/lojas`, loja))
  return lojaRest;
}

public async update(loja:Loja): Promise<Loja | undefined> {
  let lojaRest:Loja | undefined = await firstValueFrom(this.http.put<Loja>(`${environment.api}/lojas/${loja.id}`, loja))
  return lojaRest;
}

public async buscaPorId(id:Number): Promise<Loja | undefined> {
  return await firstValueFrom(this.http.get<Loja | undefined>(`${environment.api}/lojas/${id}`))
}

public excluirPorId(id:Number) {
  firstValueFrom(this.http.delete(`${environment.api}/lojas/${id}`))
}
}
