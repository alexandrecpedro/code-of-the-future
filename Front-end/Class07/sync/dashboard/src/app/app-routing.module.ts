import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: "", loadChildren: () => import("./pages/home/home.module").then(m => m.HomeModule)},
  { path: "charts", loadChildren: () => import("./pages/charts/charts.module").then(m => m.ChartsModule)},
  { path: "tables", loadChildren: () => import("./pages/tables/tables.module").then(m => m.TablesModule)},
  { path: "**", redirectTo: "", pathMatch: "prefix" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
