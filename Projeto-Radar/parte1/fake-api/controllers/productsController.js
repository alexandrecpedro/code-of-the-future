const Product = require("../models/product");

module.exports = {
    index: async (req, res, next) => {
        const products = await Product.findProducts();
        res.status(200).send(products);
    },
    create: async (req, res, next) => {
        const product = new Product(req.body);
        const products = await Product.findProducts();
        product.id = new Date().getTime();
        // product.id = products.length + 1;
        Product.save(product);
        res.status(201).send(product);
    },
    update: async (req, res, next) => {
        let productDb = await Product.findProductById(req.params.id);
        if(!productDb) return res.status(404).send({ message: "Produto não encontrado" });

        const product = new Product(req.body);
        product.id = productDb.id;
        Product.save(product);
        res.status(200).send(product);
    },
    delete: (req, res, next) => {
        Product.deleteById(req.params.id);
        res.status(204).send("");
    },
    show: async (req, res, next) => {
        let productDb = await Product.findProductById(req.params.id);
        if(!productDb) return res.status(404).send({ message: "Produto não encontrado" });
        res.status(200).send(productDb);
    }
}