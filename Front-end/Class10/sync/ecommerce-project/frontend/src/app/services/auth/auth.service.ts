import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { ApiService } from '../api/api.service';
import { TokenStorageService } from '../token-storage/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  /** ATTRIBUTES **/
  private userSubject: BehaviorSubject<any>;
  user: Observable<any>;
  
  /** CONSTRUCTOR **/
  constructor(
    private _api: ApiService,
    private _token: TokenStorageService
  ) {
    this.userSubject = new BehaviorSubject<any>(this._token.getUser());
    this.user = this.userSubject.asObservable();
  }

  /** METHODS **/
  getUser(): BehaviorSubject<any> {
    return this.userSubject.value;
  }

  login(credentials: any): Observable<any> {
    return this._api
      .postTypeRequest(`auth/login`, {
        email: credentials.email,
        password: credentials.password,
      })
      .pipe(
        map((res: any) => {
          let user = {
            email: credentials.email,
            token: res.token,
          };
          this._token.setToken(res.token);
          this._token.setUser(res.data[0]);
          this.userSubject.next(user);
          return user;
        })
      );
  }

  register(user: any): Observable<any> {
    return this._api.postTypeRequest('auth/register', {
      fullName: user.fullName,
      email: user.email,
      password: user.password,
    });
  }

  logout(): void {
    this._token.clearStorage();
    this.userSubject.next(null);
  }
}
