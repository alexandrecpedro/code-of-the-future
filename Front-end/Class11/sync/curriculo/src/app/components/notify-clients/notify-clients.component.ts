import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ObserverClientService } from 'src/app/services/observer-client/observer-client.service';

@Component({
  selector: 'app-notify-clients',
  templateUrl: './notify-clients.component.html',
  styleUrls: ['./notify-clients.component.css']
})
export class NotifyClientsComponent implements OnInit {
  /** ATTRIBUTES **/
  // @Input()
  // clientQuantity: Number = 0;

  /** CONSTRUCTOR **/
  constructor(observerClientService: ObserverClientService) { }

  /** METHODS **/
  ngOnInit(): void {
  }

}
