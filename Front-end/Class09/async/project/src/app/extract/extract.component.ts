import { Component, OnInit } from '@angular/core';
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
    console.log(error);
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
