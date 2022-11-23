import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  /** CONSTRUCTOR **/
  constructor(
    private _router: Router,
    private _token: TokenStorageService
  ) {}

  /** METHODS **/
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const currentUser = this._token.getUser();
    if (currentUser) return true;
    this._router.navigate(['/login']);
    return false;
  }
}
