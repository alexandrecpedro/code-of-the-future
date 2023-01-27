import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public email: String = "";
  public senha: String = "";
  public mensagem: String = "";

  constructor(private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
  }

  logar() {
    console.log(this.email)
    if (this.email === "radar@login.com" && this.senha === "12345") {
      localStorage.setItem("logged", "true");
      localStorage.setItem("adm", "true");
      this.loginService.notify();
      this.router.navigateByUrl('');
    
    }

    else {
      this.mensagem = "Usuário ou senha inválidos"
      this.email = ""
      this.senha = ""
    }
  }
}
