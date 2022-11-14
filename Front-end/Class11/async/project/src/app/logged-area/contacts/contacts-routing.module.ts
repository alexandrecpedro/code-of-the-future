import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DetailContactComponent } from './detail-contact/detail-contact.component';
import { EditContactComponent } from './edit-contact/edit-contact.component';
import { ListContactsComponent } from './list-contacts/list-contacts.component';

const routes: Routes = [
  { path: "", component: ListContactsComponent },
  { path: "new", component: EditContactComponent },
  { path: ":id", component: DetailContactComponent },
  { path: ":id/edit", component: EditContactComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule { }
