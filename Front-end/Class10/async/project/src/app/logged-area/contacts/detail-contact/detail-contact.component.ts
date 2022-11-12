import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize, take } from 'rxjs';

import { Contact } from '../contacts.interfaces';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-detail-contact',
  templateUrl: './detail-contact.component.html',
  styleUrls: ['./detail-contact.component.css']
})
export class DetailContactComponent implements OnInit {
  /** ATTRIBUTES **/
  contact!: Contact;
  isLoading: boolean = false;
  errorWhileLoading: boolean = false;

  /** CONSTRUCTOR **/
  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.loadContact();
  }

  loadContact(): void {
    this.isLoading = true;
    this.errorWhileLoading = false;

    const contactId = this.route.snapshot.paramMap.get("id");
    if (contactId) {
      this.contactsService.getContact(contactId)
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
  }

  onSuccess(response: Contact): void {
    this.contact = response;
  }

  onError(error: any): void {
    this.errorWhileLoading = true;
    console.error(error);
  }

  back(): void {
    this.router.navigate([`contacts`]);
  }
}
