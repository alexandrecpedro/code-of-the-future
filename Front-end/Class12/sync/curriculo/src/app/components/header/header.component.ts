import { Component, OnInit } from '@angular/core';
import { LoggedService } from 'src/app/services/logged/logged.service';
import { ObserverClientService } from 'src/app/services/observer-client/observer-client.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    public observerClientService: ObserverClientService,
    public loggedService: LoggedService,
  ) { }

  ngOnInit(): void {
  }

}
