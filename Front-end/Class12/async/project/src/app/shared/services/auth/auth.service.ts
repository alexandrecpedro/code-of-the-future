import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '../../interfaces/user.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  /** ATTRIBUTES **/
  user!: User | null;
  token!: string | null;

  /** CONSTRUCTOR **/
  constructor(
    private router: Router,
  ) { }

  /** METHODS **/
  setUser(user: User): void {
    localStorage.setItem("user", JSON.stringify(user));
  }

  getUser(): User | null {
    if (this.user) {
      return this.user;
    }

    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      this.user = JSON.parse(storedUser);
      return this.user;
    }

    return null;
  }

  setToken(token: string): void {
    this.token = token;
    localStorage.setItem("token", token);
  }

  getToken(): string {
    if (this.token) {
      return this.token;
    }

    const storedToken = localStorage.getItem("token");
    if (storedToken) {
      this.token = storedToken;
      return this.token;
    }

    // return null;
    return "";
  }

  isLogged(): boolean {
    return (this.getUser() && this.getToken()) ? true : false;
  }

  logout(): void {
    this.user = null;
    this.token = null;
    localStorage.clear();
    this.router.navigate(["login"]);
  }
}
