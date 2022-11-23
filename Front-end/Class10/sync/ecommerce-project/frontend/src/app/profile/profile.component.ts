import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../services/api/api.service';
import { TokenStorageService } from '../services/token-storage/token-storage.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  /** ATTRIBUTES **/
  user = [
    {
      key: 'fullName',
      label: 'Full name',
      value: '',
      type: 'text',
    },
    {
      key: 'email',
      label: 'Email address',
      value: '',
      type: 'email',
    },
    {
      key: 'password',
      label: 'Password',
      value: '',
      type: 'password',
    },
    {
      key: 'confirmPassword',
      label: 'Confirm password',
      value: '',
      type: 'password',
    },
  ];
  userId: (string | null) = null;
  alertMessage: string = '';
  alertType: string = '';
  alertVisible: boolean = false;
  loading: boolean = false;
  
  /** CONSTRUCTOR **/
  constructor(
    private _api: ApiService,
    private _router: Router,
    private _token: TokenStorageService
  ) {}

  /** METHODS **/
  ngOnInit(): void {
    const { user_id, fname, email } = this._token.getUser();
    this.userId = user_id;
    this.user[0].value = fname;
    this.user[1].value = email;
    console.log(this.user);
  }

  canUpdate(): boolean {
    return (this.user.filter((field) => field.value.length > 0).length !== 4) ? true : false;
  }

  // Submit data to be updated
  onSubmit(): void {
    this.alertVisible = false;
    if (this.user[2].value !== this.user[3].value) {
      this.alertType = 'error';
      this.alertMessage = 'Passwords do not match';
      this.alertVisible = true;
    } else {
      this.loading = true;
      this._api
        .putTypeRequest(`users/${this.userId}`, {
          fullName: this.user[0].value,
          email: this.user[1].value,
          password: this.user[2].value,
        })
        .subscribe(
          (response: any) => {
            console.log(response);
            this.alertMessage = response.message;
            this.alertType = 'success';
            this.alertVisible = true;
            this.loading = false;
            const oldDetails = this._token.getUser();
            this._token.setUser({
              ...oldDetails,
              fname: this.user[0].value,
              email: this.user[1].value,
            });
            this.user[2].value = '';
            this.user[3].value = '';
            // window.location.reload();
          },
          (error: any) => {
            console.log(error);
            this.alertMessage = error.error.message;
            this.alertVisible = true;
            this.alertType = 'error';
            this.loading = false;
          }
        );
    }
  }
}
