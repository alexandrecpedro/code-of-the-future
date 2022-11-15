import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { TablesComponent } from './pages/tables/tables.component';
import { ChartsComponent } from './pages/charts/charts.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TablesComponent,
    ChartsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
