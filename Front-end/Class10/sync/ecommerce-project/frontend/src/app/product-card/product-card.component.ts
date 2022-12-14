import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  /** ATTRIBUTES | DECORATORS **/
  @Input() title!: string;
  @Input() image!: string;
  @Input() short_desc!: string;
  @Input() category!: string;
  @Input() quantity!: number;
  @Input() price!: string;
  @Input() id!: number;
  @Input() onAdd: any;

  /** CONSTRUCTOR **/
  constructor() {}

  /** METHODS **/
  ngOnInit(): void {}
}
