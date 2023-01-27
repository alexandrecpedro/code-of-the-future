import { Component } from '@angular/core';
import { Product } from 'src/app/interfaces/product.interface';
import { ProductService } from 'src/app/services/product/product.service';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef } from '@angular/material/dialog';


@Component({
  selector: 'app-detail-product-dialog',
  templateUrl: './detail-product-dialog.component.html',
  styleUrls: ['./detail-product-dialog.component.css']
})
export class DetailProductDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DetailProductDialogComponent>,
  ) {}

  private productService: ProductService = {} as ProductService;
  public products: Product[] | undefined = [];
  public product: Product = {} as Product;


  selectProduct(producte: Product){
    this.product = producte;
  }

  async save(){
    if(this.product.id && this.product.id != 0){
        const update = await this.productService.updateProduct(this.product);
        console.log(update);
    }
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  faXmark = faXmark;
}
