import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { firstValueFrom, window } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Usuario } from '../models/usuario';


@Injectable({
  providedIn: 'root'
})
export class LoginService {



  constructor(private http: HttpClient,
    private router: Router,
  ) {
  }

  public logado: boolean = false
  public adm: boolean = false

  public async login(usuario:Usuario) {
    const result = await firstValueFrom(this.http.post<any>(`${environment.api}/login`, usuario));
    if (result && result.token) {
      localStorage.setItem('token', result.token);
      localStorage.setItem("logado", "true")
      return true;
    }
    return false
  }

  getAuthorizationToken(){
    const token = localStorage.getItem('token')
    return token;
  }

getTokenExpirationDate(token: string): Date{
  const decoded: any = jwtDecode(token);
  if (decoded.exp === undefined){
    return new Date(0);;
  }
const date = new Date(0);
date.setUTCSeconds(decoded.exp);
return date;
}

isTokenExpired(token?: string):boolean{
  if(!token){
    return true;
  }
  const date = this.getTokenExpirationDate(token);
  if (date === undefined){
    return false;
  }

  return !(date.valueOf() > new Date().valueOf())
}

verificaUsuarioLogado(){
  const token = this.getAuthorizationToken();
  if(!token){
    return false;
  }else if (this.isTokenExpired(token)) {
    return false
  }

  return true
}

  public confirmacao: boolean = false

  public deslogar() {
    this.confirmacao = confirm("Deseja sair?")
    if (this.confirmacao === true) {
      localStorage.clear()
      this.logado = false
      this.adm = false

      this.router.navigateByUrl("/login")
    }
    return false
  }


}
