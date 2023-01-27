module.exports = class Order {
    // ATTRIBUTES

    // CONSTRUCTOR
    constructor(order) {
        this.id = order?.id;
        this.client_id = order?.client_id;
        this.total_value = order?.total_value;
        this.date = order?.date;
    }

    // METHODS
    static async findOrders() {
        let orders = [];
        const fs = require('fs');

        try {
            const jsonOrders = await fs.readFileSync('database/orders.json', 'utf8');
            orders = JSON.parse(jsonOrders);
        } catch (error) {
            console.error(error);
        }

        return orders;
    }

    static async findOrderById(id) {
        const orderList = await this.findOrders();
        const orderDb = orderList.find((order) => order.id.toString() === id.toString());
        if (orderDb !== null && orderDb !== undefined && orderDb !== "") {
            return orderDb;
        }
        return null;
    }

    static async save(order) {
        const orderList = await this.findOrders();
        const orderDbIndex = orderList.findIndex((orderDb) => orderDb.id.toString() === order.id.toString());
        
        if (orderDbIndex === -1) {
            const objectLiteral = {...order};
            orderList.push(objectLiteral);
        } else {
            orderList[orderDbIndex] = order;
        }

        Order.saveJsonDisk(orderList);
    }

    static async deleteById(id) {
        const orderList = await this.findOrders();
        const orderDbIndex = orderList.findIndex((orderDb) => orderDb.id.toString() === id.toString());
        
        if (orderDbIndex !== -1) {
            orderList.splice(orderDbIndex, 1);
        } 

        Order.saveJsonDisk(orderList);
    }

    static async saveJsonDisk(orders) {
        const fs = require('fs');

        try {
            fs.writeFileSync('database/orders.json', JSON.stringify(orders), {encoding: 'utf8'});
        } catch (error) {
            console.error(error);
        }
    }
}