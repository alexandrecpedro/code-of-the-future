import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import ptBr from '@angular/common/locales/pt';
import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductsComponent } from './pages/products/products.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { ClientsComponent } from './pages/clients/clients.component';
import { CashFlowComponent } from './pages/cash-flow/cash-flow.component';
import { LoginComponent } from './pages/login/login.component';

import { CpfFormatPipe } from './pipes/cpf-format/cpf-format.pipe';
import { PhoneFormatPipe } from './pipes/phone-format/phone-format.pipe';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatDialogModule } from '@angular/material/dialog';
import { FormDialogComponent } from './components/form-dialog/form-dialog.component';
import { UpdateFormComponent } from './components/update-form/update-form.component';
import { ProductDialogComponent } from './components/product-dialog/product-dialog.component';
import { ClientDialogComponent } from './components/client-dialog/client-dialog.component';
import { ProductFormDialogComponent } from './components/product-form-dialog/product-form-dialog.component';
import { DetailProductDialogComponent } from './components/detail-product-dialog/detail-product-dialog.component';

import { NgChartsModule, NgChartsConfiguration } from 'ng2-charts';

registerLocaleData(ptBr);


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    ProductsComponent,
    OrdersComponent,
    ClientsComponent,
    CashFlowComponent,
    CpfFormatPipe,
    PhoneFormatPipe,
    FormDialogComponent,
    UpdateFormComponent,
    ProductDialogComponent,
    ClientDialogComponent,
    ProductFormDialogComponent,
    DetailProductDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatDialogModule,
    NgChartsModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt' },
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' },
    { provide: NgChartsConfiguration, useValue: { generateColors: false }}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
