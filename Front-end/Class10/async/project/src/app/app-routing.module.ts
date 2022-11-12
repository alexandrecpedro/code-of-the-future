import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoggedAreaComponent } from './logged-area/logged-area.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  { path: "", component: LoggedAreaComponent },
  { path: "login", component: LoginComponent },
  { path: "**", component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
