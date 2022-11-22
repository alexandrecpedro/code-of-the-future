import { CarrinhoComponent } from './paginas/carrinho/carrinho.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactListComponent } from './paginas/contact-list/contact-list.component';
import { FormComponent } from './paginas/form/form.component';
import { HomeComponent } from './paginas/home/home.component';
import { NaoEncontradaComponent } from './paginas/nao-encontrada/nao-encontrada.component';
import { PortfolioComponent } from './paginas/portfolio/portfolio.component';
import { ProdutosComponent } from './paginas/produtos/produtos.component';
import { SobreComponent } from './paginas/sobre/sobre.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'portfolio', component: PortfolioComponent },
  { path: 'sobre', component: SobreComponent },
  { path: 'form', component: FormComponent },
  { path: 'form-alterar/:id', component: FormComponent },
  { path: 'contatos', component: ContactListComponent },
  { path: 'produtos', component: ProdutosComponent },
  { path: 'carrinho', component:CarrinhoComponent },
  { path: '**', component: NaoEncontradaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
