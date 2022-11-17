import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MEMES_ORDERED_BY_CATEGORY } from './directives.constants';

@Component({
  selector: 'app-directives',
  templateUrl: './directives.component.html',
  styleUrls: ['./directives.component.scss'],
  // Angular automatically emulates an encapsulation when a property is created
  encapsulation: ViewEncapsulation.Emulated,
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

  mustBeGreen: boolean = false;

  IMAGE_URL_PREFIX = 'https://raw.githubusercontent.com/vitorfgsantos/angular-memes-diretivas/master/images';
  MEMES_ORDERED_BY_CATEGORY = MEMES_ORDERED_BY_CATEGORY;

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

  makeItGreen(): void {
    this.mustBeGreen = true;
  }

}
