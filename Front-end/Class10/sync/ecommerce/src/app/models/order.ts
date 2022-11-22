import { Client } from "./client";
import { Product } from "./product";

export class Order {
    id: Number = 0;
    clientId: Number = 0;
    itens: Product[] = [];
    orderDate: Date = new Date();

    constructor() {}

    // addProduct(product: Product) {
    //     if (this.itens.length > 0) {
    //         this.itens.push(product);
    //     }
    // }

    totalValue(): Number {
        return this.itens.reduce((accumulator, item) => accumulator + parseFloat(item.value.toString()), 0);
    }
}