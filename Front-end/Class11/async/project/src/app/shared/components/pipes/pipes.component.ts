import { DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pipes',
  templateUrl: './pipes.component.html',
  styleUrls: ['./pipes.component.scss'],
  providers: [DecimalPipe,]
})
export class PipesComponent {
  /** ATTRIBUTES **/
  movie = {
    title: "Harry Potter - Philosopher's Stone",
    rating: 4.456456456,
    rentalPrice: 15.45,
    releaseDate: new Date(2019, 5, 30),
  };

  shopping = [{
    product: "lamps",
    value: 299.29,
    quantity: 2,
    weight: 0,
    date: new Date(2020, 1, 1, 15, 20),
  }, {
    product: "flour",
    value: 450.29,
    weight: 29.33333,
    quantity: 2,
    date: new Date(2020, 1, 10, 19, 30),
  }];

  /** CONSTRUCTOR **/
  constructor(
    private decimalPipe: DecimalPipe
  ) { }

  /** METHODS **/
  // // Lifecycle Hooks
  // ngOnInit(): void {
  // };

  getFormattedWeight(weight: number): String {
    return (weight <= 0 ? "no weight" : (this.decimalPipe.transform(weight, "1.1-2") + "kg"));
  }

}
