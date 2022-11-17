import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthService } from '../../services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class IsNotLoggedGuard implements CanActivate {
  /** CONSTRUCTOR **/
  constructor(
    private authService: AuthService,
    private router: Router,
  ) { }

  /** METHODS **/
  canActivate(): boolean {
    const isLogged = this.authService.isLogged();
    
    if (!isLogged) {
      return true;
    }

    this.router.navigate(["home"]);
    return false;
  }

}
