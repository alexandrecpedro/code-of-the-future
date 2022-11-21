import { Component, OnInit } from '@angular/core';
import { ObserverClientServiceService } from 'src/app/services/observer-client-service/observer-client-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(observerClientService: ObserverClientServiceService) { }

  ngOnInit(): void {
  }

}
