import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  /** ATTRIBUTES **/
  private baseUrl = environment.apiUrl;
  
  /** CONSTRUCTOR **/
  constructor(private _http: HttpClient) {}

  /** METHODS **/
  getTypeRequest(url: string): any {
    return this._http.get(`${this.baseUrl}/${url}`)
      .pipe(
        map((res) => {
          return res;
        })
      );
  }

  postTypeRequest(url: string, payload: any): any {
    return this._http.post(`${this.baseUrl}/${url}`, payload)
      .pipe(
        map((res) => {
          return res;
        })
      );
  }

  putTypeRequest(url: string, payload: any): any {
    return this._http.put(`${this.baseUrl}/${url}`, payload)
      .pipe(
        map((res) => {
          return res;
        })
      );
  }
}
