import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  /** DECORATORS **/
  @Input() title: string = "You're welcome!";
  
  /** ATTRIBUTES  **/
  
  /** CONSTRUCTOR  **/
  constructor() { }
  
  /** METHODS  **/
  ngOnInit(): void {
  }

  logout() {}

}
