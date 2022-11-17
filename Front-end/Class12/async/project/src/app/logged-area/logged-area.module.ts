import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoggedAreaComponent } from './logged-area.component';
import { LoggedAreaRoutingModule } from './logged-area-routing.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [LoggedAreaComponent],
  imports: [
    CommonModule,
    LoggedAreaRoutingModule,
    SharedModule
  ]
})
export class LoggedAreaModule { }
