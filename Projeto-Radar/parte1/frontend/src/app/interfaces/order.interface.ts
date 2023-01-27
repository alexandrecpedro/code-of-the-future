export interface Order {
    id :Number,
    client_id :number,
    total_value :number,
    date : Date
}
export interface OrderProduct {
    id: number,
    order_id: number,
    product_id: number,
    value: number,
    quantity: number
}