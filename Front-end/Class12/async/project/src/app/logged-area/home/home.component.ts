import { Component, OnInit, TemplateRef } from '@angular/core';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

import { User } from 'src/app/shared/interfaces/user.interface';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  /** ATTRIBUTES **/
  user!: User | null;

  /** CONSTRUCTOR **/
  constructor(
    private authService: AuthService,
    private modalService: NgbModal,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.user = this.authService.getUser();
  }

  openModal(content: TemplateRef<any>): void {
    this.modalService.open(content).result.then((result) => {
      console.log("Closed modal!");
    }, (reason) => {
      console.log("Dismissed modal!");
    });
  }

}
