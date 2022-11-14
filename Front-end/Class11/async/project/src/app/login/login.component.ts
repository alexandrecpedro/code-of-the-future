import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';

import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  /** DECORATORS **/
  @ViewChild("emailInput") emailInput!: ElementRef;
  @ViewChild("passwordInput") passwordInput!: ElementRef;

  /** ATTRIBUTES **/
  email!: string;
  password!: string;

  isLoading!: boolean;
  errorOnLogin!: boolean;

  /** CONSTRUCTOR **/
  constructor(
    private loginService: LoginService,
    private router: Router,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
  }

  onSubmit(form: NgForm): void {
    this.errorOnLogin = false;
    
    if (!form.valid) {
      form.controls["email"].markAsTouched();
      form.controls["password"].markAsTouched();

      if (form.controls["email"].invalid) {
        this.emailInput.nativeElement.focus();
        return;
      }

      // if (form.controls["password"].invalid) {
        this.passwordInput.nativeElement.focus();
      //   return;
      // }

      return;
    }

    this.login();
  }

  login(): void {
    this.isLoading = true;

    this.loginService.login(this.email, this.password)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe(
        response => this.onSuccessLogin(),
        error => this.onErrorLogin(),
      );
      // .subscribe((response) => response ? this.onSuccessLogin(): this.onErrorLogin());
  }

  onSuccessLogin(): void {
    this.router.navigate(["home"]);
  }

  onErrorLogin(): void {
    this.errorOnLogin = true;
  }

  displayError(controlName: string, form: NgForm): boolean {
    if (!form.controls[controlName]) {
      return false;
    }

    return form.controls[controlName].invalid && form.controls[controlName].touched;
  }
}
