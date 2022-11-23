import { Component, OnInit } from '@angular/core';
import { finalize, pipe } from 'rxjs';
import { AuthService } from '../services/auth/auth.service';
import { CartService } from '../services/cart/cart.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  /** ATTRIBUTES **/
  currentUser: any;
  currentStep: number = 1;
  cardNumber: string = '';
  cardName: string = '';
  cardExpiry: string = '';
  cardCode: string = '';
  cartData: any;
  products: any;
  loading: boolean = false;
  successMessage: string = '';
  orderId!: number;

  billingAddress: any[] = [
    {
      name: 'Full name',
      placeholder: 'Enter your full name',
      type: 'text',
      value: '',
    },
    {
      name: 'Email',
      placeholder: 'Enter your email address',
      type: 'email',
      value: '',
    },
    {
      name: 'Address',
      placeholder: 'Enter your address',
      type: 'text',
      value: '',
    },
    {
      name: 'City',
      placeholder: 'Enter your city',
      type: 'text',
      value: '',
    },
    {
      name: 'Country',
      placeholder: 'Enter your country',
      type: 'text',
      value: '',
    },
    {
      name: 'ZIP',
      placeholder: 'Enter your zip code',
      type: 'text',
      value: '',
    },
    {
      name: 'Telephone',
      placeholder: 'Enter your telephone number',
      type: 'text',
      value: '',
    },
  ];
  
  /** CONSTRUCTOR **/
  constructor(
    private _auth: AuthService,
    private _cart: CartService
  ) {
    this._auth.user.subscribe((user) => {
      if (user) {
        this.currentUser = user;
        this.billingAddress[0].value = user.fname;
        this.billingAddress[1].value = user.email;
      }
    });

    this._cart.cartDataObs$.subscribe((cartData) => {
      this.cartData = cartData;
    });
  }

  /** METHODS **/
  ngOnInit(): void {
  }

  submitCheckout(): void {
    this.loading = true;
    setTimeout(() => {
      this._cart
        .submitCheckout(this.currentUser.user_id, this.cartData)
        .pipe(
          finalize(() => this.loading = false)
        )
        .subscribe(
          (response: any) => {
            console.log(response);
            this.orderId = response.orderId;
            this.products = response.products;
            this.currentStep = 4;
            this._cart.clearCart();
          },
          (error: any) => {
            console.log(error);
          }
        );
    }, 750);
  }

  getProgressPrecent(): number {
    return (this.currentStep / 4) * 100;
  }

  submitBilling(): void {
    this.nextStep();
  }

  canBillingSubmit(): boolean {
    return (this.billingAddress.filter((field) => field.value.length > 0).length) !== 7
      ? true
      : false;
  }

  submitPayment(): void {
    this.nextStep();
  }

  canPaymentSubmit(): boolean {
    return this.cardNumber && this.cardName && this.cardExpiry && this.cardCode
      ? true
      : false;
  }

  nextStep(): void {
    this.currentStep += 1;
    localStorage.setItem('checkoutStep', this.currentStep.toString());
  }

  prevStep(): void {
    if (this.currentStep > 1) {
      this.currentStep -= 1;
      localStorage.setItem('checkoutStep', this.currentStep.toString());
    }
  }
}
