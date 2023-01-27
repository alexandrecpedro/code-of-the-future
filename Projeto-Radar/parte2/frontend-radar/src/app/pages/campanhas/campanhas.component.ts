import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, copyArrayItem, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ProdutoServico } from 'src/app/servicos/produtoServico';
import { Produto } from 'src/app/models/produto';
import { ActivatedRoute, Router } from '@angular/router';
import { CampanhaService } from 'src/app/servicos/campanha.service';
import { PosicoesProdutoService } from 'src/app/servicos/posicoes-produto.service';
import { Campanha } from 'src/app/models/campanha';
import { PosicoesProduto } from 'src/app/models/PosicoesProduto';


@Component({
  selector: 'app-campanhas',
  templateUrl: './campanhas.component.html',
  styleUrls: ['./campanhas.component.css']
})
export class CampanhasComponent implements OnInit {

  constructor(
    private produtoService: ProdutoServico,
    private campanhaService: CampanhaService,
    private posicoesProdutoService: PosicoesProdutoService,
    private router: Router,
    private routerParams: ActivatedRoute,
  ) { }

  public produtos: Produto[] | undefined = [];
  public posicoesProdutos: PosicoesProduto[] | undefined = [];
  public prateleira: Produto[] | undefined = [];
  public prateleira2: Produto[] | undefined = [];
  public prateleira3: Produto[] | undefined = [];
  public posicaox = 0
  public posicaoy = 0
  public campanha: Campanha = {} as Campanha
  public posicaoProduto: PosicoesProduto = {} as PosicoesProduto
  produtoId:Number = 0;
  campanhaId:Number = 0;

  ngOnInit(): void {
    this.campanhaId = this.routerParams.snapshot.params['id'];
    this.campanha.id = this.campanhaId
    this.listaProdutos();
    this.getlasCampanha();
    this.popularPrateleiras();
    console.log(this.campanha.id)
  }
async getlasCampanha(){
 this.campanha = await this.campanhaService.getLast();
}

atualizarCampanha(){
  this.campanhaService.update(this.campanha);
  this.router.navigateByUrl("/campanhas");
}

criarPosicaoProduto(){
  this.posicaoProduto.posicaoX = this.posicaox
  this.posicaoProduto.posicaoY = this.posicaoy
  this.posicaoProduto.campanha_id = this.campanhaId
  this.posicaoProduto.produto_id = this.produtoId
  this.posicoesProdutoService.criar(this.posicaoProduto);
}

async popularPrateleiras(){
  this.posicoesProdutos = await this.posicoesProdutoService.listaComProduto(this.campanha.id);
  this.posicoesProdutos.forEach(produto => {
    this.prateleira2.push(produto.produto)
    this.prateleira3.push(produto.produto)
    if(this.prateleira.length < 3){
      this.prateleira.push(produto.produto)
    }
  });
    this.prateleira2.splice(6, 7)
    this.prateleira2.shift()
    this.prateleira2.shift()
    this.prateleira2.shift()
    this.prateleira3.splice(0,6)

}
  getElement(produto:string, id:Number){
    var posicao: HTMLElement = document.getElementById(produto); 
    var posicaoDom: DOMRect = posicao.getBoundingClientRect();
    this.posicaox = posicaoDom.x
    this.posicaoy = posicaoDom.y
    this.produtoId = id
    this.criarPosicaoProduto();
  }
 async listaProdutos(){
 this.produtos = await this.produtoService.lista();
  }

  drop(event: CdkDragDrop<Produto[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
        );
    }
  }
}

