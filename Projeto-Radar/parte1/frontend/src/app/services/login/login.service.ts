import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  public logged: boolean = false;
  public adm: boolean = false;

  constructor(private router: Router) {
    this.notify();
  }

  public verifyLogged(): boolean {
    this.notify();
    return this.logged;
  }

  public notify() {
    this.logged = localStorage.getItem("logged") ? true : false;
    this.adm = localStorage.getItem("adm") ? true : false;
  }

  public logout() {
    localStorage.clear();
    this.logged = false;
    this.adm = false;
    this.router.navigateByUrl("/");
  }
}