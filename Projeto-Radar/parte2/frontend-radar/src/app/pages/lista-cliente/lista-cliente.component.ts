import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente';
import { ClienteServico } from 'src/app/servicos/clienteServico';


@Component({
  selector: 'app-lista-cliente',
  templateUrl: './lista-cliente.component.html',
  styleUrls: ['./lista-cliente.component.css']
})
export class ListaClienteComponent implements OnInit {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) { }

  private clienteServico: ClienteServico = {} as ClienteServico
  public clientes: Cliente[] | undefined = [];

  ngOnInit(): void {
    this.clienteServico = new ClienteServico(this.http);
    this.listaDeClientes();
  }
  
  novoCliente(){
    this.router.navigateByUrl("/form-clientes");
  }

  private async listaDeClientes(){
    this.clientes = await this.clienteServico.lista();
  }

  async excluir(cliente:Cliente){
    if(confirm("Tem certeza que deseja excluir esse cliente?")){
      await this.clienteServico.excluirPorId(cliente.id)
      this.clientes = await this.clienteServico.lista()
    }
  }
}
