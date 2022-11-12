import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  /** DECORATORS **/
  /** ATTRIBUTES  **/
  @Input() title: string = "You're welcome!";

  /** CONSTRUCTOR  **/
  constructor() {
  }

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

  logout() { }
}
