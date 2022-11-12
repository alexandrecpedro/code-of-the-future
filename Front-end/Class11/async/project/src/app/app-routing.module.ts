import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  { path: "", 
    loadChildren: () => import("./logged-area/logged-area.module").then(m => m.LoggedAreaModule),
    canActivate: []
  },
  { path: "login", 
    component: LoginComponent, 
    canActivate: []
  },
  { path: "**", component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
