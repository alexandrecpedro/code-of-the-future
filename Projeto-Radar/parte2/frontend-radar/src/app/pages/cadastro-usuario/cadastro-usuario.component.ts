import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/models/usuario';
import { LoginService } from 'src/app/servicos/login.service';
import { UsuarioService } from 'src/app/servicos/usuario.service';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.css']
})
export class CadastroUsuarioComponent implements OnInit {

  constructor(
    private router:Router,
    private usuarioService: UsuarioService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
  }
  public usuario:Usuario = {} as Usuario;
  public nome:String = ""
  public email:String = ""
  public senha:String = ""
  public confirmaSenha: String = ""
  public mensagem:string = ""

  async cadastrar(){
    try{
      this.usuario.email = this.email;
      this.usuario.senha = this.senha;
      this.usuario.nome = this.nome;
      this.usuario.permissao = "editor"
      if(this.senha === this.confirmaSenha){
      const result = await this.usuarioService.criar(this.usuario);
      this.router.navigateByUrl("/login")
      }else this.mensagem = "As senhas devem ser iguais"
    }catch (error){
      console.error(error)
      this.mensagem = "As senhas devem ser iguais"
    }

  }

}


