import { NgModel } from "@angular/forms"

export interface Pedido {
    id: Number
    cliente_id: Number
    valor_total: Number
    data: Date,
    cliente: {
        nome: string
    }
}