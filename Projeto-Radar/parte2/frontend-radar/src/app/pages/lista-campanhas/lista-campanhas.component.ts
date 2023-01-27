import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from 'express';
import { Campanha } from 'src/app/models/campanha';
import { CampanhaService } from 'src/app/servicos/campanha.service';

@Component({
  selector: 'app-lista-campanhas',
  templateUrl: './lista-campanhas.component.html',
  styleUrls: ['./lista-campanhas.component.css'],
})
export class ListaCampanhasComponent implements OnInit, OnChanges{
  constructor(
    private campanhaService: CampanhaService,
    private modalService: NgbModal
  ) {}


  public campanha:Campanha | undefined = {} as Campanha;

  ngOnInit(): void {
    this.listarCampanhas();
  
  }
  ngOnChanges(): void {
    this.listarCampanhas();
    
  }


  public campanhas: Campanha[] | undefined = [];

  public async listarCampanhas() {
    this.campanhas = await this.campanhaService.lista();
  }

 excluir(campanha: Campanha) {
    if (confirm('Tem certeza que deseja excluir esta loja?')) {
      this.campanhaService.excluirPorId(campanha.id);
     this.listarCampanhas();

    
    }
  }

  adicionarCampanha(){
    this.campanhaService.criar(this.campanha);
  }
  

  openModal(content: any) {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          console.log('Modal Closed');
        },
        (reason) => {
          console.log('Modal Dimissed');
        }
      );
  }
}
