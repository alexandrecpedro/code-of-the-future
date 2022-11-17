import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ExtractComponent } from './extract.component';

const routes: Routes = [{ path: "", component: ExtractComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExtractRoutingModule { }
