import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { ApiService } from 'src/app/services/api/api.service';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  /** ATTRIBUTES **/
  fullName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  loading: boolean = false;
  
  /** CONSTRUCTOR **/
  constructor(
    private _api: ApiService,
    private _auth: AuthService,
    private _router: Router
  ) {}

  /** METHODS **/
  ngOnInit(): void {}

  onSubmit(): void {
    this.errorMessage = '';
    if (this.fullName && this.password && this.email && this.confirmPassword) {
      if (this.password !== this.confirmPassword) {
        this.errorMessage = 'Passwords need to match';
      } else {
        this.loading = true;
        this._auth
          .register({
            fullName: this.fullName,
            email: this.email,
            password: this.password,
          })
          .pipe(
            finalize(() => this.loading = false)
          )
          .subscribe(
            (response: any) => {
              console.log(response);
              this._router.navigate(['/login']);
            },
            (error: any) => this.errorMessage = error.error.message
          )
      }
    } else {
      this.errorMessage = 'Make sure to fill everything ;)';
    }
  }

  canSubmit(): boolean {
    return (this.fullName && this.email && this.password && this.confirmPassword) ? true : false;
  }
}
