import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoggedService {
  /** ATTRIBUTES **/
  logged: boolean = false;
  adm: boolean = false;

  /** CONSTRUCTOR **/
  constructor(private router: Router) {
    this.notify();
  }

  /** METHODS **/
  verifyLogged(): boolean {
    this.notify();
    return this.logged;
  }

  notify() {
    this.logged = localStorage.getItem("logged") ? true : false;
    this.adm = localStorage.getItem("adm") ? true : false;
  }

  logout() {
    // localStorage.removeItem("logged");
    localStorage.clear();
    this.logged = false;
    this.adm = false;
    this.router.navigateByUrl("/");
  }

  // redirectNotLoggedLogin(): boolean {
  //   if (!this.verifyLogged()) {
  //     this.router.navigateByUrl("/login");
  //     return true;
  //   }
  //   return false;
  // }
}
