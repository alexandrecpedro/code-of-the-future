import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ContactsRoutingModule } from './contacts-routing.module';
import { DetailContactComponent } from './detail-contact/detail-contact.component';
import { EditContactComponent } from './edit-contact/edit-contact.component';
import { ListContactsComponent } from './list-contacts/list-contacts.component';

@NgModule({
  declarations: [
    DetailContactComponent,
    EditContactComponent,
    ListContactsComponent
  ],
  imports: [
    CommonModule,
    ContactsRoutingModule,
    ReactiveFormsModule
  ]
})
export class ContactsModule { }
