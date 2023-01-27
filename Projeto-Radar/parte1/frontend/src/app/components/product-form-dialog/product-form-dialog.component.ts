import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from 'src/app/interfaces/product.interface';
import { ProductObserverService } from 'src/app/services/product/product-observer.service';
import { ProductService } from 'src/app/services/product/product.service';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-form-dialog',
  templateUrl: './product-form-dialog.component.html',
  styleUrls: ['./product-form-dialog.component.css']
})
export class ProductFormDialogComponent {

  constructor(
    private http: HttpClient,
    private productObserver: ProductObserverService,
    public dialogRef: MatDialogRef<ProductFormDialogComponent>,
  ) { }

  ngOnInit(): void {
    this.productService = new ProductService(this.http);
    this.getProducts();
  }

  private productService: ProductService = {} as ProductService;
  public products: Product[] | undefined = [];
  public product: Product = {} as Product;

  private async getProducts() {
    this.products = await this.productService.getProduct();
  }


  create() {
    this.productService.createProduct({
      id: 0,
      name: this.product.name,
      description: this.product.description,
      stockQty: this.product.stockQty,
      value: this.product.value
    });
    this.productObserver.updateQty();
    this.getProducts();
    location.reload();
  }


  closeDialog(): void {
    this.dialogRef.close();
  }

  faXmark = faXmark;
}
