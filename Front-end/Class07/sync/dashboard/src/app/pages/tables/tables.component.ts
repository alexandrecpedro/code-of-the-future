import { Component, OnInit } from '@angular/core';
import { Product } from './tables.interfaces';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.css']
})
export class TablesComponent implements OnInit {
  /** ATTRIBUTES **/
  products: Product[] = [];

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  ngOnInit(): void {
    this.products.push({
      id: 1,
      name: "Banana",
      description: "Cavendish banana",
      value: 5.5
    },
    {
      id: 2,
      name: "Apple",
      description: "Apple pear",
      value: 8.5
    });
  }

}
