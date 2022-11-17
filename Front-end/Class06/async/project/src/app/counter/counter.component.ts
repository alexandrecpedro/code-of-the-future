import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.scss']
})
export class CounterComponent implements OnInit {
  /** ATTRIBUTES **/
  @Input() counterValue: number = 0;
  @Output() counterValueChange: any = new EventEmitter();

  /** CONSTRUCTOR **/
  constructor() {}
  
  /** METHODS **/
  ngOnInit(): void {
  }

  increase(): void {
    this.counterValue++;
    this.counterValueChange.emit(this.counterValue);
  }

  decrement(): void {
    this.counterValue--;
    this.counterValueChange.emit(this.counterValue);
  }

}
