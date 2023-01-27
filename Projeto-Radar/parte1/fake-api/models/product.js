module.exports = class Product {
    // ATTRIBUTES

    // CONSTRUCTOR
    constructor(product) {
        this.id = product?.id;
        this.name = product?.name;
        this.description = product?.description;
        this.value = product?.value;
        this.stockQty = product?.stockQty;
    }

    // METHODS
    static async findProducts() {
        let products = [];
        const fs = require('fs');

        try {
            const jsonProducts = await fs.readFileSync('database/products.json', 'utf8');
            products = JSON.parse(jsonProducts);
        } catch (error) {
            console.error(error);
        }

        return products;
    }

    static async findProductById(id) {
        const productList = await this.findProducts();
        const productDb = productList.find((product) => product.id.toString() === id.toString());
        if (productDb !== null && productDb !== undefined && productDb !== "") {
            return productDb;
        }
        return null;
    }

    static async save(product) {
        const productList = await this.findProducts();
        const productDbIndex = productList.findIndex((productDb) => productDb.id.toString() === product.id.toString());
        
        if (productDbIndex === -1) {
            const objectLiteral = {...product};
            productList.push(objectLiteral);
        } else {
            productList[productDbIndex] = product;
        }

        Product.saveJsonDisk(productList);
    }

    static async deleteById(id) {
        const productList = await this.findProducts();
        const productDbIndex = productList.findIndex((productDb) => productDb.id.toString() === id.toString());
        
        if (productDbIndex !== -1) {
            productList.splice(productDbIndex, 1);
        } 

        Product.saveJsonDisk(productList);
    }

    static async saveJsonDisk(products) {
        const fs = require('fs');

        try {
            fs.writeFileSync('database/products.json', JSON.stringify(products), {encoding: 'utf8'});
        } catch (error) {
            console.error(error);
        }
    }
}