import { registerLocaleData } from '@angular/common';
import ptBr from "@angular/common/locales/pt";
import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { CpfFormatPipe } from './pipes/cpf-format/cpf-format.pipe';
import { PhoneFormatPipe } from './pipes/phone-format/phone-format.pipe';
import { ProductsComponent } from './products/products.component';

registerLocaleData(ptBr);

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    PhoneFormatPipe,
    CpfFormatPipe,
    ProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: "pt" },
    { provide: DEFAULT_CURRENCY_CODE, useValue: "BRL"}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
