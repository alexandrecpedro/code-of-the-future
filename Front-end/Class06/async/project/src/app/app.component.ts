import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  /** ATTRIBUTES **/
  title = "You're welcome! =)";
  favoriteCar: string = "Ferrari";
  favoriteColor: string = "yellow";

  /** CONSTRUCTOR **/
  constructor() {
    setTimeout(() => {
      this.title="Get out of here!";
    }, 5000)
  }

  /** METHODS **/
}
