import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import localePt from '@angular/common/locales/pt';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';;
import { LoginComponent } from './login/login.component';
import { ExtractComponent } from './extract/extract.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { SharedModule } from './shared/shared.module';
import { ContactsComponent } from './contacts/contacts.component';
import { ListContactsComponent } from './contacts/list-contacts/list-contacts.component';
import { EditContactComponent } from './contacts/edit-contact/edit-contact.component';
import { DetailContactComponent } from './contacts/detail-contact/detail-contact.component';

registerLocaleData(localePt, 'pt');

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ExtractComponent,
    HomeComponent,
    NotFoundComponent,
    ContactsComponent,
    ListContactsComponent,
    EditContactComponent,
    DetailContactComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [{
    // Locale to be used => Brazilian Portuguese
    provide: LOCALE_ID,
    useValue: 'pt'
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
