import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../token-storage/token-storage.service';

const TOKEN_HEADER_KEY = 'x-access-token';

@Injectable(/*{
  providedIn: 'root'
}*/)
export class InterceptorService implements HttpInterceptor {
  /** ATTRIBUTES **/
  
  /** CONSTRUCTOR **/
  constructor(private _token: TokenStorageService) {}

  /** METHODS **/
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      let authReq = req;
      let token = this._token.getToken();

      if (token !== null) {
        authReq = req.clone({
          headers: req.headers.set(TOKEN_HEADER_KEY, token),
        });
      }
      return next.handle(authReq);
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
];
