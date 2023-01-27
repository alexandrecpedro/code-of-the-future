import { Component } from '@angular/core';
import { ClientObserverService } from 'src/app/services/client/client-observer.service';
import { OrderObserverService } from 'src/app/services/order/order-observer.service';
import { ProductObserverService } from 'src/app/services/product/product-observer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(public clientObserver: ClientObserverService, public productObserver: ProductObserverService, public orderObserver: OrderObserverService) { }
}
