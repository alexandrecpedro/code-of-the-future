import { Component, OnInit } from '@angular/core';
import { ClienteObserverServicoService } from 'src/app/servicos/clienteObserverServico.service';

@Component({
  selector: 'app-clientes-notificacao',
  templateUrl: './clientes-notificacao.component.html',
  styleUrls: ['./clientes-notificacao.component.css']
})
export class ClientesNotificacaoComponent implements OnInit {

  constructor(public clienteObserverServicoService: ClienteObserverServicoService) {
  }


  ngOnInit(): void {
  }

}
