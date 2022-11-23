import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { CartService } from '../services/cart/cart.service';
import { TokenStorageService } from '../services/token-storage/token-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  /** ATTRIBUTES **/
  screenHeight: any;
  screenWidth: any;
  isMenuOpen = false;
  isMobile = false;
  isLoggedIn = false;
  dropdownVisible = false;
  cartData: any;

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    if (this.screenWidth > 768) this.isMobile = false;
    else this.isMobile = true;
  }

  /** CONSTRUCTOR **/
  constructor(
    private _auth: AuthService,
    private _cart: CartService,
    private _token: TokenStorageService
  ) {
    this.getScreenSize();
    this._auth.user.subscribe((user) => {
      if (user) this.isLoggedIn = true;
      else this.isLoggedIn = false;
    });
    this._cart.cartDataObs$.subscribe((cartData) => {
      this.cartData = cartData;
    });
  }

  /** METHODS **/
  ngOnInit(): void {
      if (this._token.getUser()) this.isLoggedIn = true;
      else this.isLoggedIn = false;
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleDropdown(): void {
    this.dropdownVisible = !this.dropdownVisible;
  }

  removeProductFromCart(id: number): void {
    this._cart.removeProduct(id);
  }

  logout(): void {
    this._auth.logout();
    this.isMenuOpen = false;
  }
}
