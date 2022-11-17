import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoggedAreaComponent } from './logged-area.component';

const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "", component: LoggedAreaComponent , children: [
    { path: "home", loadChildren: () => import("./home/home.module").then(m => m.HomeModule)},
    { path: "extract", loadChildren: () => import("./extract/extract.module").then(m => m.ExtractModule)},
    { path: "contacts", loadChildren: () => import("./contacts/contacts.module").then(m => m.ContactsModule)}
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoggedAreaRoutingModule { }
