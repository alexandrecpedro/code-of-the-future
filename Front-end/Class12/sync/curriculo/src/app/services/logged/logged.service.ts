import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoggedService {
  /** ATTRIBUTES **/
  logged: boolean = false;

  /** CONSTRUCTOR **/
  constructor(private router: Router) {
    this.verifyLogged();
  }

  /** METHODS **/
  verifyLogged(): boolean {
    this.logged = localStorage.getItem("logged") ? true : false;
    return this.logged;
  }

  logout() {
    localStorage.removeItem("logged");
    this.logged = false;
    this.router.navigateByUrl("/");
  }

  redirectNotLoggedLogin(): boolean {
    if (!this.verifyLogged()) {
      this.router.navigateByUrl("/login");
      return true;
    }
    return false;
  }
}
