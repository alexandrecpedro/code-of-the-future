import { Client } from "./client";
import { Product } from "./product";

export class Order {
    id: Number;
    clientId: Number;
    itens: Product[] = [];
    orderDate: Date = new Date();

    constructor (client: Client) {
        this.id = this.itens.length + 1;
        this.clientId = client.id;
    }

    addProduct(product: Product) {
        if (this.itens.length > 0) {
            this.itens.push(product);
        }
    }

    totalValue(): Number {
        return this.itens.reduce((accumulator, item) => accumulator + parseFloat(item.value.toString()), 0);
    }
}