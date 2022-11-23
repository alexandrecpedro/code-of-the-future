import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  /** ATTRIBUTES **/
  email: string = '';
  password: string = '';
  error: string = '';
  loading: boolean = false;

  /** CONSTRUCTOR **/
  constructor(
    private _auth: AuthService,
    private _router: Router
  ) {}

  /** METHODS **/
  ngOnInit(): void {}

  onSubmit(): void {
    this.loading = true;
    if (!this.email || !this.password) {
      this.error = 'Make sure to fill everything ;)';
    } else {
      this._auth
        .login({ email: this.email, password: this.password })
        .pipe(
          finalize(() => this.loading = false)
        )
        .subscribe(
          (response: any) => this._router.navigate(['/']),
          (error: any) => this.error = error.error.message
        )
    }
  }

  canSubmit(): boolean {
    return this.email.length > 0 && this.password.length > 0;
  }
}
