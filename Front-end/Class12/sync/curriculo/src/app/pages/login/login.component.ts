import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router"
import { LoggedService } from 'src/app/services/logged/logged.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  /** ATTRIBUTES **/
  public email: String = "";
  public password: String = "";
  public message: String = "";

  /** CONSTRUCTOR **/
  constructor(
    private loggedService: LoggedService,
    private router: Router
  ) { }

  /** METHODS **/
  ngOnInit(): void {
  }

  login() {
    if (this.email === "danilo@students.com" && this.password === "123456") {
      localStorage.setItem("logged", "true");
      localStorage.setItem("adm", "true");
      this.loggedService.notify();
      this.router.navigateByUrl("/contacts");
    } else if (this.email === "erika@students.com" && this.password === "123456") {
      localStorage.setItem("logged", "true");
      this.loggedService.notify();
      this.router.navigateByUrl("/contacts");
    } else {
      this.message = "Invalid user or password";
      this.email = "";
      this.password = "";
    }
  }
}
