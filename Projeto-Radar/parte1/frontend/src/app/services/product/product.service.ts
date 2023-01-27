import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Product } from 'src/app/interfaces/product.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public async getProduct(): Promise<Product[] | undefined>{
    let products:Product[] | undefined = await firstValueFrom(this.http.get<Product[]>(`${environment.api}/products`));
    return products;
  }

  public async getProductById(id: number): Promise<Product | undefined>{
    let product:Product | undefined = await firstValueFrom(this.http.get<Product>(`${environment.api}/products/${id}`));
    return product;
  }

  public async createProduct(product: Product): Promise<Product | undefined>{
    let newProduct:Product | undefined = await firstValueFrom(this.http.post<Product>(`${environment.api}/products`, product));
    return newProduct; 
  }

  public async deleteProduct(productId: Number){
    await firstValueFrom(this.http.delete(`${environment.api}/Products/${productId}`));
  }

  public async updateProduct(product: Product): Promise<Product | undefined>{
    let ProductUpdate: Product | undefined = await firstValueFrom(this.http.put<Product>(`${environment.api}/Products/${product.id}`, product ));
    return ProductUpdate;
  }


}
