import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CounterComponent } from './components/counter/counter.component';
import { DataBindingComponent } from './components/data-binding/data-binding.component';
import { DirectivesComponent } from './components/directives/directives.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { PipesComponent } from './components/pipes/pipes.component';

@NgModule({
  declarations: [
    CounterComponent,
    DataBindingComponent,
    DirectivesComponent,
    FooterComponent,
    HeaderComponent,
    PipesComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    CounterComponent,
    DataBindingComponent,
    DirectivesComponent,
    FooterComponent,
    HeaderComponent,
    PipesComponent
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }]
})
export class SharedModule { }
