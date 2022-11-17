import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExtractComponent } from './extract.component';
import { ExtractRoutingModule } from './extract-routing.module';

@NgModule({
  declarations: [ExtractComponent],
  imports: [
    CommonModule,
    ExtractRoutingModule
  ]
})
export class ExtractModule { }
