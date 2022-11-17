import { Component, OnInit } from '@angular/core';
import { finalize, take } from 'rxjs';

import { Transaction } from './extract.interfaces';
import { ExtractService } from './extract.service';

@Component({
  selector: 'app-extract',
  templateUrl: './extract.component.html',
  styleUrls: ['./extract.component.scss']
})
export class ExtractComponent implements OnInit {
  /** ATTRIBUTES **/
  transactions: Transaction[] = [];
  page: number = 1;

  isLoading: boolean = false;
  errorWhileLoading: boolean = false;

  /** CONSTRUCTOR **/
  constructor(
    private extractService: ExtractService
  ) { }

  /** METHODS **/
  ngOnInit(): void {
    this.loadExtract();
  }

  loadExtract(): void {
    this.isLoading = true;
    this.errorWhileLoading = false;

    this.extractService.getTransactions(this.page)
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

  onSuccess(response: Transaction[]): void {
    this.transactions = response;
  }

  onError(error: any): void {
    this.errorWhileLoading = true;
    console.error(error);
  }

  nextPage(): void {
    this.page = this.page + 1;
    this.loadExtract();
  }

  previousPage(): void {
    this.page = this.page - 1;
    this.loadExtract();
  }
}
