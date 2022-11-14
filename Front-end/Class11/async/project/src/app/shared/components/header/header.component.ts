import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  /** DECORATORS **/
  /** ATTRIBUTES  **/
  @Input() title: string = "You're welcome!";

  /** CONSTRUCTOR  **/
  constructor(
    private authService: AuthService,
  ) { }

  /** METHODS  **/
  /* // Lifecycle Hooks
  ngOnChanges(): void {
    console.log("ngOnChanges");
  }

  ngOnInit(): void {
    console.log("ngOnInit");
  }

  ngAfterViewInit(): void {
    console.log("ngAfterViewInit");
  }

  ngOnDestroy(): void {
    console.log("I was destroyed!");
  } */

  logout(): void {
    this.authService.logout();
  }
}
