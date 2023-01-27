const Client = require("../models/client");

module.exports = {
    index: async (req, res, next) => {
        const clients = await Client.findClients();
        res.status(200).send(clients);
    },
    create: async (req, res, next) => {
        const client = new Client(req.body);
        const clients = await Client.findClients();
        client.id = new Date().getTime();
        // client.id = clients.length + 1;
        Client.save(client);
        res.status(201).send(client);
    },
    update: async (req, res, next) => {
        let clientDb = await Client.findClientById(req.params.id);
        if(!clientDb) return res.status(404).send({ message: "Cliente não encontrado" });

        const client = new Client(req.body);
        client.id = clientDb.id;
        Client.save(client);
        res.status(200).send(client);
    },
    delete: (req, res, next) => {
        Client.deleteById(req.params.id);
        res.status(204).send("");
    },
    show: async (req, res, next) => {
        let clientDb = await Client.findClientById(req.params.id);
        if(!clientDb) return res.status(404).send({ message: "Cliente não encontrado" });
        res.status(200).send(clientDb);
    }
}