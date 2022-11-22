import { Component, OnInit } from '@angular/core';
import { ObserverClientService } from 'src/app/services/observer-client-service/observer-client-service.service';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    observerClientService: ObserverClientService,
    orders: OrderService
  ) { }

  ngOnInit(): void {
  }

}
