const Order = require("../models/order");
const formatDate = require("date-and-time");

module.exports = {
    index: async (req, res, next) => {
        const orders = await Order.findOrders();
        res.status(200).send(orders);
    },
    create: async (req, res, next) => {
        const order = new Order(req.body);
        const orders = await Order.findOrders();
        order.id = new Date().getTime();
        // order.id = orders.length + 1;
        order.date = new Date();
        Order.save(order);
        res.status(201).send(order);
    },
    update: async (req, res, next) => {
        let orderDb = await Order.findOrderById(req.params.id);
        if(!orderDb) return res.status(404).send({ message: "Pedido não encontrado" });

        const order = new Order(req.body);
        order.id = orderDb.id;
        order.date = orderDb.date;
        Order.save(order);
        res.status(200).send(order);
    },
    delete: (req, res, next) => {
        Order.deleteById(req.params.id);
        res.status(204).send("");
    },
    show: async (req, res, next) => {
        let orderDb = await Order.findOrderById(req.params.id);
        if(!orderDb) return res.status(404).send({ message: "Pedido não encontrado" });
        res.status(200).send(orderDb);
    }
}