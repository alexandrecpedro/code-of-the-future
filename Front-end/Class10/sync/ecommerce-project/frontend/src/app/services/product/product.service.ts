import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product, Products } from 'src/app/shared/models/product.model';
import { environment } from 'src/environments/environment';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  /** ATTRIBUTES **/
  private url = environment.apiUrl;
  
  /** CONSTRUCTOR **/
  constructor(
    private http: HttpClient,
    private _api: ApiService
  ) {}

  /** METHODS **/
  getAllProducts(limitOfResults: number = 9, page: number): Observable<Products> {
    return this.http.get<Products>(`${this.url}/products`, {
      params: {
        limit: limitOfResults.toString(),
        page: page,
      }
    });
  }

  getSingleProduct(id: number): Observable<Product> {
    return this._api.getTypeRequest(`products/${id}`);
  }
}
