import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-data-binding',
  templateUrl: './data-binding.component.html',
  styleUrls: ['./data-binding.component.css']
})
export class DataBindingComponent implements OnInit {
  /** DECORATORS **/
  // Receive information from parent to son
  @Input() car: string | undefined;
  @Input() color: string | undefined;
  // Send information from son to parent
  @Output() clicked = new EventEmitter();

  /** ATTRIBUTES  **/
  imageURL: string = "https://aws1.discourse-cdn.com/infiniteflight/original/3X/0/5/057027cbdb2f71ee8884185adf6cac9a98e5637a.jpeg";
  initialValue: string = "Initial value";
  isDisabled: Boolean = true;
  accessibilityText: string = "An accessible text";
  inputValue: string = "";
  counterValue: number = 10;

  /** CONSTRUCTOR **/
  constructor() {
    setTimeout(() => {
      this.isDisabled = false;
      console.log(`isDisabled = ${this.isDisabled}`);
    }, 3000);
  }

  /** METHODS **/
  ngOnInit(): void {
  }

  getImageURL(): string {
    return this.imageURL;
  }

  onClick($event: Event): void {
    console.log("You have clicked!", $event);
  }

  enteredSomething($event: any): void {
    this.inputValue = $event.target.value;
    console.log($event);
  }

  mouseEnter(): void {
    console.log("Somebody has hovered the mouse");
  }

  onClickEmitButton($event: any) {
    console.log("I should output information to parent component");
    this.clicked.emit($event);
  }

  // onUpdatedValueOnCounter(newValue: number): void {
  //   this.counterValue = newValue;
  //   console.log(`onUpdatedValueOnCounter: ${newValue}`);
  // }
}
