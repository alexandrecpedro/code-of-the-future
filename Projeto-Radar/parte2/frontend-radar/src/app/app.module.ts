import { NgModule, DEFAULT_CURRENCY_CODE, LOCALE_ID,  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainRoutingModule } from '../app/main/main-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ListaClienteComponent } from './pages/lista-cliente/lista-cliente.component';
import { FormClienteComponent } from './pages/form-cliente/form-cliente.component';
import { HeaderComponent } from './pages/navegacao/header/header.component';
import { ProdutosComponent } from './pages/produtos/produtos.component';
import { FormProdutosComponent } from './pages/form-produtos/form-produtos.component';
import ptBr from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { CarrinhoComponent } from './pages/carrinho/carrinho.component';
import { FormsModule } from '@angular/forms';
import { FooterComponent } from './pages/navegacao/footer/footer.component';
import { TelefonePipe } from './pipes/telefone.pipe';
import { CpfPipe } from './pipes/cpf.pipe';
import { CepPipe } from './pipes/cep.pipe';
import { LoginComponent } from './pages/login/login.component';
import { LoginGuard } from './servicos/login.guard';
import { NotFoundComponent } from './pages/navegacao/not-found/not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { CarrinhoNotificacaoComponent } from './pages/carrinho-notificacao/carrinho-notificacao.component';
import { ListaPedidosCompletaComponent } from './pages/lista-pedidos-completa/lista-pedidos-completa.component';
import { ListaPedidosComponent } from './pages/lista-pedidos/lista-pedidos.component';
import { FormComprarProdutoComponent } from './pages/form-comprar-produto/form-comprar-produto.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { httpInterceptorProviders } from './servicos/Interceptor';
import { DetalhesLojaComponent } from './pages/detalhes-loja/detalhes-loja.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { FormLojaComponent } from './pages/form-loja/form-loja.component';
import { ListaLojasComponent } from './pages/lista-lojas/lista-lojas.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CadastroUsuarioComponent } from './pages/cadastro-usuario/cadastro-usuario.component';
import { CampanhasComponent } from './pages/campanhas/campanhas.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { ListaCampanhasComponent } from './pages/lista-campanhas/lista-campanhas.component';
registerLocaleData(ptBr);

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ListaClienteComponent,
    ProdutosComponent,
    FormProdutosComponent,
    CarrinhoComponent,
    FormClienteComponent,
    FooterComponent,
    TelefonePipe,
    CpfPipe,
    CepPipe,
    LoginComponent,
    NotFoundComponent,
    HomeComponent,
    CarrinhoNotificacaoComponent,
    ListaPedidosCompletaComponent,
    ListaPedidosComponent,
    FormComprarProdutoComponent,
    DetalhesLojaComponent,
    FormLojaComponent,
    ListaLojasComponent,
    CadastroUsuarioComponent,
    CampanhasComponent,
    ListaCampanhasComponent,
    
  ],
  imports: [
    GoogleChartsModule,
    BrowserModule,
    MainRoutingModule,
    HttpClientModule,
    FormsModule,
    GoogleMapsModule,
    NgbModule,
    DragDropModule,
  ],
  providers: [   
    { provide: LOCALE_ID, useValue: 'pt' },
    {
      provide: DEFAULT_CURRENCY_CODE,
      useValue: 'BRL',
    },
     LoginGuard,
     httpInterceptorProviders
  ],
  bootstrap: [AppComponent]

})
export class AppModule { }
