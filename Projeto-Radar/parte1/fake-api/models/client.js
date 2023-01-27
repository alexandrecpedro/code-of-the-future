module.exports = class Client {
    // ATTRIBUTES

    // CONSTRUCTOR
    constructor(client) {
        this.id = client?.id;
        this.name = client?.name;
        this.phone = client?.phone;
        this.email = client?.email;
        this.cpf = client?.cpf;
        this.cep = client?.cep;
        this.logradouro = client?.logradouro;
        this.numero = client?.numero;
        this.bairro = client?.bairro;
        this.cidade = client?.cidade;
        this.estado = client?.estado;
        this.complemento = client?.complemento;
    }

    // METHODS
    static async findClients() {
        let clients = [];
        const fs = require('fs');

        try {
            const jsonClients = await fs.readFileSync('database/clients.json', 'utf8');
            clients = JSON.parse(jsonClients);
        } catch (error) {
            console.error(error);
        }

        return clients;
    }

    static async findClientById(id) {
        const clientList = await this.findClients();
        const clientDb = clientList.find((client) => client.id.toString() === id.toString());
        if (clientDb !== null && clientDb !== undefined && clientDb !== "") {
            return clientDb;
        }
        return null;
    }

    static async save(client) {
        const clientList = await this.findClients();
        const clientDbIndex = clientList.findIndex((clientDb) => clientDb.id.toString() === client.id.toString());
        
        if (clientDbIndex === -1) {
            const objectLiteral = {...client};
            clientList.push(objectLiteral);
        } else {
            clientList[clientDbIndex] = client;
        }

        Client.saveJsonDisk(clientList);
    }

    static async deleteById(id) {
        const clientList = await this.findClients();
        const clientDbIndex = clientList.findIndex((clientDb) => clientDb.id.toString() === id.toString());
        
        if (clientDbIndex !== -1) {
            clientList.splice(clientDbIndex, 1);
        } 

        Client.saveJsonDisk(clientList);
    }

    static async saveJsonDisk(clients) {
        const fs = require('fs');

        try {
            fs.writeFileSync('database/clients.json', JSON.stringify(clients), {encoding: 'utf8'});
        } catch (error) {
            console.error(error);
        }
    }
}