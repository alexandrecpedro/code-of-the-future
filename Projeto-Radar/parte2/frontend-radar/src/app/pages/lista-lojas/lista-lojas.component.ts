import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Loja } from 'src/app/models/loja';
import { LojaService } from 'src/app/servicos/loja.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-lista-lojas',
  templateUrl: './lista-lojas.component.html',
  styleUrls: ['./lista-lojas.component.css']
})
export class ListaLojasComponent implements OnInit {

  constructor(
    private router: Router,
    private lojaService: LojaService,
    private modalService : NgbModal
  ) { }

  ngOnInit(): void {
    this.listaLojas();
  }

  public lojas: Loja[] | undefined= [];
  latitude:Number = 0 
  longitude:Number = 0


  novaLoja(){
    this.router.navigateByUrl("/form-loja");
  }

  public async listaLojas(){
    this.lojas = await this.lojaService.lista();
    this.lojas.forEach(loja => {
      this.latitude = loja.latitude
      this.longitude = loja.longitude
    });
  }

  
  async excluir(loja:Loja){
    if(confirm("Tem certeza que deseja excluir esta loja?")){
      await this.lojaService.excluirPorId(loja.id)
      this.lojas = await this.lojaService.lista()
    }
}

openModal(content:any) {
  this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
    (result) => {
      console.log("Modal Closed")
    },
    (reason) => {
      console.log("Modal Dimissed")
    },
  );
}

options: google.maps.MapOptions = {
  center: {lat: 41.40338, lng: - 2.17403},
  zoom: 15
};

}
