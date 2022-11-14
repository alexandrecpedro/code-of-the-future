import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";
import { finalize, Observable, take } from 'rxjs';

import { Contact } from '../contacts.interfaces';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.scss']
})
export class EditContactComponent implements OnInit {
  /** ATTRIBUTES **/
  contactId!: string;
  contactForm!: FormGroup;

  isLoading!: boolean;
  errorWhileLoading!: boolean;

  /** CONSTRUCTOR **/
  constructor(
    private formBuilder: FormBuilder,
    private contactsService: ContactsService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.initializeForm();

    // this.contactId = this.route.snapshot.paramMap.get("id");

    this.isEditing() && this.loadContact();
  }

  isEditing = () => Boolean(this.contactId);

  initializeForm(): void {
    this.contactForm = this.formBuilder.group({
      name: ["", Validators.required],
      cpf: ["", Validators.required],
      // bankData: this.formBuilder({
      bank: ["", Validators.required],
      agency: ["", [Validators.required, Validators.minLength(4)]],
      cc: ["", [Validators.required, Validators.minLength(5)]],
      // })
    });
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
        response => this.onSuccessLoadContact(response),
        error => this.onErrorLoadContact(error),
      );
    }
  }

  onSuccessLoadContact(response: Contact): void {
    this.contactForm.patchValue(response);
  }

  onErrorLoadContact(error: any): void {
    this.errorWhileLoading = true;
    console.error(error);
  }

  displayError(controlName: string) {
    if (!this.contactForm.get(controlName)) return false;

    return this.contactForm.get(controlName)?.invalid && this.contactForm.get(controlName)?.touched
  }

  validateFieldForms(form: FormGroup): void {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched();
      } else if (control instanceof FormGroup) {
        this.validateFieldForms(control);
      }
    });
  }

  onSubmit(): void {
    if (this.contactForm.invalid) {
      this.validateFieldForms(this.contactForm);
      return;
    }

    if (this.isEditing()) {
      this.saveContact();
      return;
    }

    this.createContact();
  }

  saveContact(): void {
    this.contactsService.updateContact(this.contactId, this.contactForm.value)
      .subscribe(
        response => this.onSuccessSaveContact(),
        error => this.onError()
      );
  }

  onSuccessSaveContact(): void {
    this.toastr.success("Success", "Successfully edited contact!");
    this.router.navigate([`contacts`]);
  }

  createContact(): void {
    this.contactsService.createContact(this.contactForm.value)
      .subscribe(
        response => this.onSuccessCreateContact(),
        error => this.onError(),
      );
  }

  onSuccessCreateContact(): void {
    this.toastr.success("Success", "Successfully created contact!");
    this.router.navigate([`contacts`]);
  }

  onError(): void {
    this.toastr.error("Error!", "Something has got wrong!");
  }

}
