import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DataBindingComponent } from './shared/components/data-binding/data-binding.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { CounterComponent } from './shared/components/counter/counter.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { DirectivesComponent } from './shared/components/directives/directives.component';
import { PipesComponent } from './shared/components/pipes/pipes.component';

registerLocaleData(localePt, 'pt');

@NgModule({
  declarations: [
    AppComponent,
    DataBindingComponent,
    HeaderComponent,
    CounterComponent,
    FooterComponent,
    DirectivesComponent,
    PipesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [{
    // Locale to be used => Brazilian Portuguese
    provide: LOCALE_ID,
    useValue: 'pt'
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
