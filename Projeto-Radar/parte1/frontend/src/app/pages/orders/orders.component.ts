
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { faCirclePlus,faInfoCircle, faTrashCan,faPenToSquare} from '@fortawesome/free-solid-svg-icons';
import { ClientDialogComponent } from 'src/app/components/client-dialog/client-dialog.component';
import { ProductDialogComponent } from 'src/app/components/product-dialog/product-dialog.component';
import { Client } from 'src/app/interfaces/client.interface';
import { Order, OrderProduct } from 'src/app/interfaces/order.interface';
import { Product } from 'src/app/interfaces/product.interface';
import { ClientService } from 'src/app/services/client/client.service';
import { OrderObserverService } from 'src/app/services/order/order-observer.service';
import { OrderService } from 'src/app/services/order/order.service';
import { ProductService } from 'src/app/services/product/product.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit{
  constructor(
    private dialogRef : MatDialog,
    public orderObserver: OrderObserverService,
    private http: HttpClient,
    private routerParams: ActivatedRoute,
    private router: Router
    ){

    }

  private orderService: OrderService = {} as OrderService;
  private productService: ProductService = {} as ProductService;
  private clientService: ClientService = {} as ClientService;
  public orders: Order[] | undefined= []
  public orderslist: Order[] | undefined= []

  public isCreating: boolean = false;
  public isEdit: boolean = false;
  public isList: boolean = true;
  public titulo:String = "Novo Pedido";


  faCirclePlus = faCirclePlus;
  faInfo = faInfoCircle;
  faTrashCan = faTrashCan;
  faPenToSquare = faPenToSquare;

  setQty: number = 0;
  stock: number = 0;
  date: Date = new Date();

  public async getOrders(){
     this.orders = await this.orderService.getOrder(); 
     this.orderslist = this.orders?.reverse();
  }


  ngOnInit(): void{
    this.orderService = new OrderService(this.http);
    this.productService = new ProductService(this.http);
    this.clientService = new ClientService(this.http);
    this.getOrders();
    this.orderObserver.productsOrdered = [];
    this.orderObserver.orderClient = {} as Client;
    this.orderObserver.profileMock = ""
    let id:number = this.routerParams.snapshot.params['id']
    if(id){
      this.isEdit = true;
      this.editOrder(id);
    }
  }

  private async editOrder(id:number){
    this.getOrders();
    this.titulo = `Pedido #${id}`
    this.isList = false;
    let orderUpdate = await this.orderService.getOrderById(id);
    let client;
    if (orderUpdate){
      this.date = orderUpdate?.order.date;
      client = await this.clientService.getClientbyId(orderUpdate?.order.client_id);
    }

    if(client){
      this.orderObserver.setClient(client);
    }
    
    orderUpdate?.orderProducts.map(async productOrdered=>{
      let product = await this.productService.getProductById(productOrdered.product_id);
      if(product){
        this.orderObserver.productsOrdered.push({qty: productOrdered.quantity, product: product})
      }
  
    });
  }
  openDialog(){
    this.dialogRef.open(ProductDialogComponent,{
    });
  }

  openClient(){
    this.dialogRef.open(ClientDialogComponent,{});
  }

  plusQty(product: String, qty: number){
    const index = this.orderObserver.productsOrdered.map(e=>e.product.name).indexOf(product);
    const stock = this.orderObserver.productsOrdered.map(e=>e.product.stockQty);
    if(stock[0]<=this.setQty) return;
    this.setQty = qty+1;
    this.orderObserver.updateProductOrdered(index, this.setQty);
  }
  minusQty(product: String, qty: number){
    if(qty==1) return
    this.setQty = qty - 1;
    const index = this.orderObserver.productsOrdered.map(e=>e.product.name).indexOf(product);
    this.orderObserver.updateProductOrdered(index, this.setQty);
  }

  delete(product: Product){
    const result = this.orderObserver.productsOrdered.filter(e=>e.product.name != product.name);
    this.orderObserver.delete(result);
  }

  public creating(){
    this.isCreating = !this.isCreating;
    this.isList = !this.isList;
    this.getOrders();
  }
  public redirect(id: Number){
    this.router.navigateByUrl(`orders/${id}`);
  }
  async save(){
    let newProductOrdered: OrderProduct[] = [];
    let newOrder: Order = {
      id: 0,
      client_id: this.orderObserver.orderClient.id,
      date: new Date(),
      total_value: this.orderObserver.sumValue()
    }
    this.orderObserver.productsOrdered.map(productOrdered =>{
      newProductOrdered.push({
        id: 0,
        product_id: productOrdered.product.id,
        order_id: 0,
        quantity: productOrdered.qty,
        value: productOrdered.product.value
      });
    });

   const saveOrder = await this.orderService.createOrder(newOrder, newProductOrdered);
   this.getOrders();
    this.creating();
  }


}
