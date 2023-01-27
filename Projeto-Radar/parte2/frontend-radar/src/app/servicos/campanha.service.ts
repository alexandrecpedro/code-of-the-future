import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Campanha } from '../models/campanha';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CampanhaService {

  constructor(private http: HttpClient) { }

  public async lista(): Promise<Campanha[] | undefined> {
    let campanhas:Campanha[] | undefined = await firstValueFrom(this.http.get<Campanha[]>(`${environment.api}/campanhas`))
    return campanhas;
}

public async criar(campanha:Campanha): Promise<Campanha | undefined> {
    let campanhaRest:Campanha | undefined = await firstValueFrom(this.http.post<Campanha>(`${environment.api}/campanhas`, campanha))
    return campanhaRest;
}

public async update(campanha:Campanha): Promise<Campanha | undefined> {
    let campanhaRest:Campanha | undefined = await firstValueFrom(this.http.put<Campanha>(`${environment.api}/campanhas/${campanha.id}`, campanha))
    return campanhaRest;
}

public async buscaPorId(id:Number): Promise<Campanha | undefined> {
    return await firstValueFrom(this.http.get<Campanha | undefined>(`${environment.api}/campanhas/${id}`))
}

public excluirPorId(id:Number) {
    firstValueFrom(this.http.delete(`${environment.api}/campanhas/${id}`))
}

public async getLast(): Promise<Campanha | undefined> {
    let campanha = await firstValueFrom(this.http.get<Campanha>(`${environment.api}/campanhasLast`))
    return campanha;
}
}
