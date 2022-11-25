module.exports = class Client {
    constructor(client) {
        this.id = client?.id;
        this.name = client?.name;
        this.cpf = client?.cpf;
        this.phone = client?.phone;
        this.address = client?.address;
        this.value = client?.value;
    }

    /** METHODS **/
    static async listClients() {
        let clients = [];
        const fs = require("fs");
        try {
            const jsonClients = await fs.readFileSync("db/clients.json", "utf8");
            clients = JSON.parse(jsonClients);
        } catch (error) {
            console.log(error);
        }
        return clients;
    }

    static async save(client) {
        const clientsList = await this.listClients();
        let exist = false;

        for (let i = 0; i < clientsList.length; i++) {
            const clientDb = clientsList[i];
            if (clientDb.id === client.id) {
                clientDb.name = client.name;
                clientDb.cpf = client.cpf;
                clientDb.phone = client.phone;
                clientDb.address = client.address;
                clientDb.value = client.value;
                exist = true;
                break;
            }
        }

        if (!exist) {
            const literalObject = {...client};
            clientsList.push(literalObject);
        }

        Client.saveJsonDisk(clientsList);
    }

    static async findById(id) {
        const clientsList = await this.listClients();

        let exist = false;

        for (let i = 0; i < clientsList.length; i++) {
            const clientDb = clientsList[i];
            if (clientDb.id.toString() === id.toString()) {
                return clientDb;
            }
        }
        return null;
    }

    static async deleteById(id) {
        const clientsList = await this.listClients();
        const newList = [];
        for (let i = 0; i < clientsList.length; i++) {
            const clientDb = clientsList[i];
            if (clientDb.id.toString() !== id.toString()) {
                newList.push(clientDb);
            }
        }

        Client.saveJsonDisk(newList);
    }

    static async saveJsonDisk(clients) {
        const fs = require("fs");
        try {
            fs.writeFileSync("db/clients.json", JSON.stringify(clients), {encoding: "utf8"});
            clients = JSON.parse(jsonClients);
        } catch (error) {
            console.log(error);
        }
    }
}