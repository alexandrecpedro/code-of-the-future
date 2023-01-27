import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Client } from 'src/app/interfaces/client.interface';
import { Order, OrderProduct } from 'src/app/interfaces/order.interface';
import { ClientService } from 'src/app/services/client/client.service';
import { OrderService } from 'src/app/services/order/order.service';




@Component({
  selector: 'app-cash-flow',
  templateUrl: './cash-flow.component.html',
  styleUrls: ['./cash-flow.component.css']
})
export class CashFlowComponent implements OnInit{
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  constructor(
    private orderService: OrderService, 
    private clientService: ClientService,
    private http: HttpClient,

    ){}
  
  public orders: Order[] | undefined= [];

  public lastOrder: Order | undefined= {} as Order;
  public lastOrderClient: Client | undefined = {} as Client;
  public lastOrdererdProcuct: OrderProduct[] | undefined = [];

  public totalOrderProducts: OrderProduct[] | undefined = [];

  public thisYear:[{month: number, value: number}] = [{}] as [{month: number, value: number}];
  public lastYear:[{month: number, value: number}] = [{}] as [{month: number, value: number}];
  public thisYearData:number[] = [0,0,0,0,0,0,0,0,0,0,0,0];
  public lastYearData:number[] = [0,0,0,0,0,0,0,0,0,0,0,0];
  public thisYearRevenue: number = 0;
  public lastYearRevenue: number = 0;
  public percentage: number = 0;
    
  
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    
    scales: {
      x: {},
      y: {
        min: 10
      }
    },
    plugins: {
      legend: {
        display: true,
      }
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartData: ChartData<'bar'> = {
    labels: [ 'Janeiro','Fevereiro', 'Março', 'Abril','Maio' , 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro' ],
    datasets: [
      { data: this.lastYearData, label: 'Ano Anterior' , borderRadius:2, borderColor:'#680BEA', backgroundColor:'#680BEA'},
      { data: this.thisYearData, label: 'Esse Ano',borderColor:'#a776eb', backgroundColor:'#a776eb' }
    ]
  };

  public async getOrders(){
    this.orders = await this.orderService.getOrder(); 
    
    this.orders?.map(order=>{
      let parseDate = new Date(order.date);
      parseDate.getFullYear() == 2022 ? this.thisYear.push({month: parseDate.getMonth(), value: order.total_value}) : this.lastYear.push({month: parseDate.getMonth(), value: order.total_value});
    });
    this.lastYear.map(month=>{
      if(month.month>=0){
        this.lastYearData[month.month] += month.value;
      }
    });
    this.thisYear.map(month=>{
      if(month.month>=0){
        this.thisYearData[month.month] += month.value
      }
    });
    this.thisYearRevenue = this.thisYearData.reduce((total, number) => total + number, 0);
    this.lastYearRevenue = this.lastYearData.reduce((total, number)=> total + number, 0);
    this.chart?.update();
    this.getData();
    this.getLastClient()
 }

 public async getTotalOrderedProducts(){
  this.totalOrderProducts = await this.orderService.getOrderProduct();
 }
public async getLastClient(){
  if(this.orders){
    let aux = this.orders.reverse();
    this.lastOrder = aux[0];
    this.lastOrderClient = await this.clientService.getClientbyId(Number(this.lastOrder.client_id));
  }
}
 public getData(){
  this.percentage =(this.thisYearRevenue - this.lastYearRevenue) / this.lastYearRevenue * 100
 }

  ngOnInit(): void {
    this.orderService = new OrderService(this.http);
    this.clientService = new ClientService(this.http);
    this.getOrders();
    this.getTotalOrderedProducts();
    this.getLastClient()
    this.chart?.update();
  }

  





  public randomize(): void {
    // Only Change 3 values
    this.barChartData.datasets[0].data = [
      Math.round(Math.random() * 100),
      59,
      80,
      Math.round(Math.random() * 100),
      56,
      Math.round(Math.random() * 100),
      40 ];

    this.chart?.update();
  }
}