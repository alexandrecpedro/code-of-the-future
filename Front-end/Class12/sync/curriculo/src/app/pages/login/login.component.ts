import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router"

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  /** ATTRIBUTES **/
  email: String = "";
  password: String = "";
  message: String = "";

  /** CONSTRUCTOR **/
  constructor(private router: Router) { }

  /** METHODS **/
  ngOnInit(): void {
  }

  login() {
    if (this.email === "alexandrepedro@classroom.com" && this.password === "123456") {
      localStorage.setItem("logged", "true");
      this.router.navigateByUrl("/contacts");
    } else {
      this.message = "Invalid user or password";
      this.email = "";
      this.password = "";
    }
  }
}
