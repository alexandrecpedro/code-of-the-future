import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-directives',
  templateUrl: './directives.component.html',
  styleUrls: ['./directives.component.css']
})
export class DirectivesComponent implements OnInit {
  /** ATTRIBUTES **/
  mustDisplay: boolean = true;
  fruitList: string[] = [
    "Apple", 
    "Orange",
    "Pawpaw",
    "Pear",
  ];
  carList = [
    {
      licensePlate: "JND-7438",
      color: "Black",
    },
    {
      licensePlate: "JGG-2039",
      color: "Blue",
    },
    {
      licensePlate: "JND-1230",
      color: "White",
    },
    {
      licensePlate: "OGK-7095",
      color: "Red",
    },
  ];

  /** CONSTRUCTOR **/
  constructor() { }

  /** METHODS **/
  ngOnInit(): void {
  }

  changeValue(): void {
    this.mustDisplay = !this.mustDisplay;
  }

  sum(number1: number, number2: number): number {
    return number1 + number2;
  }

}
