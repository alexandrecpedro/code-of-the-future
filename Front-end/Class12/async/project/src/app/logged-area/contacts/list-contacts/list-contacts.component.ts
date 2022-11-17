import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { finalize, Observable, take } from 'rxjs';

import { Contact } from '../contacts.interfaces';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-list-contacts',
  templateUrl: './list-contacts.component.html',
  styleUrls: ['./list-contacts.component.scss']
})
export class ListContactsComponent implements OnInit {
  /** ATTRIBUTES **/
  contacts: Contact[] = [];
  isLoading!: boolean;
  errorWhileLoading!: boolean;

  /** CONSTRUCTOR **/
  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private toastr: ToastrService,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.isLoading = true;
    this.errorWhileLoading = false;

    this.contactsService.getContacts()
      // Operators from RxJS
      .pipe(
        // Observable send only 1 event then unsubscribe from Observable
        take(1),
        // Finalize = when function ends
        finalize(() => this.isLoading = false)
      )
      .subscribe(
        response => this.onSuccess(response),
        error => this.onError(error),
      );
  }

  onSuccess(response: Contact[]): void {
    this.contacts = response;
  }

  onError(error: any): void {
    this.errorWhileLoading = true;
    console.error(error);
  }

  goToDetails(contactId: number): void {
    this.router.navigate([`contacts/${contactId}`]);
  }

  goToEdit(contactId: number): void {
    this.router.navigate([`contacts/${contactId}/edit`]);
  }

  deleteContact(contactId: number): void {
    this.contactsService.deleteContact(contactId.toString())
      .subscribe(
        response => this.onSuccessDeleteContact(contactId),
        error => this.onErrorDeleteContact()
      );
  }

  onSuccessDeleteContact(contactId: number): void {
    this.toastr.success("Success!", "Successfully deleted contact.");
    this.contacts = this.contacts.filter(contact => contact.id !== contactId);
  }

  onErrorDeleteContact(): void {
    console.error("Error when deleting contact!");
  }

  newContact(): void {
    this.router.navigate([`contacts/new`]);
  }
}
