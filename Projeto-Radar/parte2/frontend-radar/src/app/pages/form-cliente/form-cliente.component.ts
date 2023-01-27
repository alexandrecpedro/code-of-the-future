import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VirtualTimeScheduler } from 'rxjs';
import { Cliente } from 'src/app/models/cliente';
import { Estado } from 'src/app/models/estado';
import { Municipio } from 'src/app/models/municipio';
import { ClienteServico } from 'src/app/servicos/clienteServico';
import { IBGEServico } from 'src/app/servicos/IBGEServico';

@Component({
  selector: 'app-form-cliente',
  templateUrl: './form-cliente.component.html',
  styleUrls: ['./form-cliente.component.css']
})
export class FormClienteComponent implements OnInit {

  constructor(
    private router: Router,
    private http: HttpClient,
    private routerParams: ActivatedRoute,
  ) { }

  public tituloDoBotao:String = "Cadastrar";
  private clienteServico: ClienteServico = {} as ClienteServico;
  public cliente:Cliente | undefined = {} as Cliente;
  public IBGEServico: IBGEServico={} as IBGEServico;
  public estados:Estado[]=[];
  public municipios:Municipio[]|undefined=[];
  public estadoSelecionado: String="1- Acre";
  public municipioSelecionado: String="1- ";
  ngOnInit(): void {
    this.clienteServico = new ClienteServico(this.http);
    this.IBGEServico= new IBGEServico(this.http);
    let id:Number = this.routerParams.snapshot.params['id'];
    if(id){
      this.editaCliente(id);
    }
    this.importarEstados();
    console.log(this.estadoSelecionado)
    console.log(this.municipioSelecionado)
  }

  private async importarEstados(){
    let estados = await this.IBGEServico.listaEstados();
    if(!estados){}else{
      this.estados=estados;
    }
    this.importarCidades();
  }

  public async importarCidades(){
    this.municipios= await this.IBGEServico.listaMunicipiosPorEstado(Number(this.estados.at(Number(this.estadoSelecionado.split("-")[0])-1)?.id));
   
    this.municipioSelecionado="1- ";
    console.log(this.estadoSelecionado)
    console.log(this.municipioSelecionado)
  }

  private async editaCliente(id:Number){
    this.tituloDoBotao = "Alterar";
    this.cliente = await this.clienteServico.buscaPorId(id);
  }

   async registrar(){
    if(this.cliente && this.cliente.id > 0){
      this.cliente.estado=this.estadoSelecionado.split("-")[1].trim()
      this.cliente.cidade=this.municipioSelecionado.split("-")[1].trim()
      let cliente = this.verificaUndefined()
        if(cliente){
          await this.clienteServico.update(cliente);
          this.router.navigateByUrl("/clientes");
        }
    }
    else{
      if(!this.cliente){}
      else{
        let cliente = this.verificaUndefined()
        if(cliente){
          console.log(cliente)
          this.cliente.estado=this.estadoSelecionado.split("-")[1].trim()
          this.cliente.cidade=this.municipioSelecionado.split("-")[1].trim()
          await this.clienteServico.criar(this.cliente);
          this.router.navigateByUrl("/clientes");
        }
      }
    }
  }

  verificaUndefined(){
    let id:Number = 0;
    let nome:String = "";
    let telefone:String = "";
    let email:String = "";
    let cpf:String = "";
    let cep:String = "";
    let logradouro:String = "";
    let bairro:String ="";
    let numero:Number = 0;
    let cidade:String= ""
    let tipoCidade=this.municipios?.at(Number(this.municipioSelecionado.split("-")[0])-1)?.nome;
    let estado:String= this.estadoSelecionado.split("-")[1].trim();

    let complemento:String = "";

    if(this.cliente?.id) id = this.cliente.id;

    if(this.cliente?.nome && !(this.cliente.nome ==="")) nome = this.cliente.nome;
    else{
      alert("Por favor digite um nome válido");
      return undefined
    }
    if(this.cliente?.telefone && !(this.cliente.telefone === "")) telefone = this.cliente.telefone;
    else{
      alert("Por favor digite um telefone válido");
      return undefined
    }
    if(this.cliente?.email && !(this.cliente.email === "")) email = this.cliente.email;
    else{
      alert("Por favor digite um email válido");
      return undefined
    }
    if(this.cliente?.cpf && !(this.cliente.cpf === "")) cpf = this.cliente.cpf;
    else{
      alert("Por favor digite um CPF válido");
      return undefined
    }
    if(this.cliente?.cep && !(this.cliente.cep === "")) cep = this.cliente.cep;
    else{
      alert("Por favor digite um CEP válido");
      return undefined
    }
    if(this.cliente?.logradouro && !(this.cliente.logradouro === "")) logradouro = this.cliente.logradouro;
    else{
      alert("Por favor digite um logradouro válido");
      return undefined
    }
    if(this.cliente?.numero && !(this.cliente.numero === 0)) numero = this.cliente.numero;
    else{
      alert("Por favor digite um número válido");
      return undefined
    }
    if(this.cliente?.bairro && !(this.cliente.bairro === "")) bairro = this.cliente.bairro;
    else{
      alert("Por favor digite um bairro válido");
      return undefined
    }
    if(tipoCidade) cidade = tipoCidade;
    if(this.cliente?.estado) estado = this.cliente.estado;
    if(this.cliente?.complemento) complemento = this.cliente.complemento;

    let cliente = {
      id: id,
      nome: nome,
      telefone: telefone,
      email: email,
      cpf: cpf,
      cep: cep,
      logradouro: logradouro,
      numero: numero,
      bairro: bairro,
      cidade: cidade,
      estado: estado,
      complemento: complemento,
    }

    return cliente
    }
  number(val:String){
    return Number(val);
  }
}
