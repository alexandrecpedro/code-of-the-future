import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/usuario';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient) { }



  public async criar(usuario:Usuario): Promise<Usuario | undefined> {
    let usuarioRest:Usuario | undefined = await firstValueFrom(this.http.post<Usuario>(`${environment.api}/usuarios/`, usuario))
    return usuarioRest;
}

public async update(usuario:Usuario): Promise<Usuario | undefined> {
  let usuarioRest:Usuario | undefined = await firstValueFrom(this.http.put<Usuario>(`${environment.api}/usuarios/${usuario.id}`, usuario))
  return usuarioRest;
}

public async buscaPorId(id:Number): Promise<Usuario | undefined> {
  return await firstValueFrom(this.http.get<Usuario | undefined>(`${environment.api}/usuarios/${id}`))
}

public excluirPorId(id:Number) {
  firstValueFrom(this.http.delete(`${environment.api}/usuarios/${id}`))
}
}
