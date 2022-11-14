import { Injectable } from '@angular/core';
import { delay, mergeMap, Observable, of, tap, throwError, timer } from 'rxjs';

import { AuthService } from '../shared/services/auth/auth.service';
import { LoginResponse } from './login.interfaces';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  /** ATTRIBUTES **/

  /** CONSTRUCTOR **/
  constructor(
    private authService: AuthService,
  ) { }

  /** METHODS **/
  login(email: string, password: string): Observable<LoginResponse> {
    if (email === "vitorfgsantos@outlook.com" && password === "123") {
      return of({
        user: {
          name: "Vitor",
          lastName: "Farias",
          email: "vitorfgsantos@outlook.com"
        },
        token: "aD12h3123523543fgdfg",
      })
        .pipe(
          delay(2000),
          // tap(response => {
          //   this.authService.setUser(response.user);
          //   this.authService.setToken(response.token);
          // })
          tap(({ user, token }) => {
            this.authService.setUser(user);
            this.authService.setToken(token);
          })
        );
    }

    return timer(3000).pipe(
      mergeMap(() => throwError(() => new Error("Invalid user or password!")))
    );
  }
}
