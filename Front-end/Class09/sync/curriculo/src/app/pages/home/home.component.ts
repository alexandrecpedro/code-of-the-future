import { Component, OnInit } from '@angular/core';
import { Client } from '../../models/client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  /** ATTRIBUTES **/

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  ngOnInit(): void {
    // this.client = this.clients[0];
  }

}
