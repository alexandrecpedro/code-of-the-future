const OrderProduct = require("../models/orderProduct");

module.exports = {
    index: async (req, res, next) => {
        const orderProducts = await OrderProduct.findOrderProducts();
        res.status(200).send(orderProducts);
    },
    create: async (req, res, next) => {
        const orderProduct = new OrderProduct(req.body);
        const orderProducts = await OrderProduct.findOrderProducts();
        orderProduct.id = new Date().getTime();
        // orderProduct.id = orderProducts.length + 1;
        OrderProduct.save(orderProduct);
        res.status(201).send(orderProduct);
    },
    update: async (req, res, next) => {
        let orderProductDb = await OrderProduct.findOrderProductById(req.params.id);
        if(!orderProductDb) return res.status(404).send({ message: "PedidoProduto não encontrado" });

        const orderProduct = new OrderProduct(req.body);
        orderProduct.id = orderProductDb.id;
        OrderProduct.save(orderProduct);
        res.status(200).send(orderProduct);
    },
    delete: (req, res, next) => {
        OrderProduct.deleteById(req.params.id);
        res.status(204).send("");
    },
    show: async (req, res, next) => {
        let orderProductDb = await OrderProduct.findOrderProductById(req.params.id);
        if(!orderProductDb) return res.status(404).send({ message: "PedidoProduto não encontrado" });
        res.status(200).send(orderProductDb);
    }
}