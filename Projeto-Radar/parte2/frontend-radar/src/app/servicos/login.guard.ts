import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  constructor(
    private router:Router,
    private loginService: LoginService
  ){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        if(this.loginService.verificaUsuarioLogado()){
          return true;
        }else{
          this.router.navigateByUrl("/login");
          return false
        }


        // if(!this.logadoService.logado){
        //   this.router.navigateByUrl("/login")
        // return false

        // }
        // return true
  }
  
}