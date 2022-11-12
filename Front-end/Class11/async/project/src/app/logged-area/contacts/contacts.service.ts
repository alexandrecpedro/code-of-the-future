import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { Contact } from './contacts.interfaces';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  /** ATTRIBUTES **/
  API_URL = environment.API_URL;

  /** CONSTRUCTOR **/
  constructor(
    private http: HttpClient,
  ) { }

  /** METHODS **/
  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.API_URL}/contacts`);
  }

  getContact(contactId: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.API_URL}/contacts/${contactId}`);
  }

  createContact(contact: Contact): Observable<Contact[]> {
    return this.http.post<Contact[]>(`${this.API_URL}/contacts`, contact);
  }

  updateContact(contactId: string, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${this.API_URL}/contacts/${contactId}`, contact);
  }

  deleteContact(contactId: string): Observable<Contact> {
    return this.http.delete<Contact>(`${this.API_URL}/contacts/${contactId}`);
  }
}
