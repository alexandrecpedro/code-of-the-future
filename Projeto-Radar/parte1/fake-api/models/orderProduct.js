module.exports = class OrderProduct {
    // ATTRIBUTES

    // CONSTRUCTOR
    constructor(orderProduct) {
        this.id = orderProduct?.id;
        this.order_id = orderProduct?.order_id;
        this.product_id = orderProduct?.product_id;
        this.value = orderProduct?.value;
        this.quantity = orderProduct?.quantity;
    }

    // METHODS
    static async findOrderProducts() {
        let orderProducts = [];
        const fs = require('fs');

        try {
            const jsonOrderProducts = await fs.readFileSync('database/orderProducts.json', 'utf8');
            orderProducts = JSON.parse(jsonOrderProducts);
        } catch (error) {
            console.error(error);
        }

        return orderProducts;
    }

    static async findOrderProductById(id) {
        const orderProductList = await this.findOrderProducts();
        const orderProductDb = orderProductList.find((orderProduct) => orderProduct.id.toString() === id.toString());
        if (orderProductDb !== null && orderProductDb !== undefined && orderProductDb !== "") {
            return orderProductDb;
        }
        return null;
    }

    static async save(orderProduct) {
        const orderProductList = await this.findOrderProducts();
        const orderProductDbIndex = orderProductList.findIndex((orderProductDb) => orderProductDb.id.toString() === orderProduct.id.toString());
        
        if (orderProductDbIndex === -1) {
            const objectLiteral = {...orderProduct};
            orderProductList.push(objectLiteral);
        } else {
            orderProductList[orderProductDbIndex] = orderProduct;
        }

        OrderProduct.saveJsonDisk(orderProductList);
    }

    static async deleteById(id) {
        const orderProductList = await this.findOrderProducts();
        const orderProductDbIndex = orderProductList.findIndex((orderProductDb) => orderProductDb.id.toString() === id.toString());
        
        if (orderProductDbIndex !== -1) {
            orderProductList.splice(orderProductDbIndex, 1);
        } 

        OrderProduct.saveJsonDisk(orderProductList);
    }

    static async saveJsonDisk(orderProducts) {
        const fs = require('fs');

        try {
            fs.writeFileSync('database/orderProducts.json', JSON.stringify(orderProducts), {encoding: 'utf8'});
        } catch (error) {
            console.error(error);
        }
    }
}