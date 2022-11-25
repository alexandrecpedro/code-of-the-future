/* GET home page. */
module.exports = {
    index: async (req, res, next) => {
        const clients = await Client.listClients();
        res.status(200).send(clients);
    },
    create: (req, res, next) => {
        const client = new Client(req.body);
        client.id = new Date().getTime();
        Client.save(client);
        res.status(201).send("");
    },
    update: async (req, res, next) => {
        let clientDb = await Client.findById(req.params.id);
        if (!clientDb) return res.status(404).send({ message: "Client not found "});

        const client = new Client(req.body);
        client.id = clientDb.id;
        Client.save(client);
        res.status(200).send("");
    },
    delete: (req, res, next) => {
        Client.deleteById(req.params.id);
        res.status(204).send("");
    },
    show: async (req, res, next) => {
        let clientDb = await Client.findById(req.params.id);
        if (!clientDb) return res.status(404).send({ message: "Client not found "});
        res.status(200).send(clientDb);
    },
};
