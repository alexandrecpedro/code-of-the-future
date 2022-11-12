import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { mergeMap, Observable, throwError, timer } from "rxjs";
import { environment } from "src/environments/environment";

import { Transaction } from "./extract.interfaces";

@Injectable({
  providedIn: 'root'
})
export class ExtractService {
  /** ATTRIBUTES **/
  API_URL = environment.API_URL;

  /** CONSTRUCTOR **/
  constructor(
    private http: HttpClient,
  ) { }

  /** METHODS **/
  getTransactions(page: number): Observable<Transaction[]> {
    // Simulate an error
    // const error = throwError(() => new Error("Generic error"));
    // return timer(3000).pipe(mergeMap(() => error));

    return this.http.get<Transaction[]>(`${this.API_URL}/transactions`, {
      params: {
        _page: String(page)
      }
    });
  }
}
