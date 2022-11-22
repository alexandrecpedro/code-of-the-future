import { Cliente } from "../models/cliente";

export class ClienteServico{

    static buscaClientePorId(id: Number): Cliente {
        let cliente:Cliente = {} as Cliente

        for(let i=0; i<ClienteServico.clientes.length; i++){
            let clienteDb = ClienteServico.clientes[i]
            if(clienteDb.id == id){
                cliente = clienteDb
                break
            }
        }

        return cliente;
    }

    private static clientes: Cliente[] = [{
        id: 1,
        nome: "Luana",
        telefone: 1212111-1111,
        endereco: "Rua teste",
        data: new Date(),
        valor: 22.22,
        cpf: "333.222.222-33"
    }]

    public static buscaClientes():Cliente[]{
        return ClienteServico.clientes
    }

    public static adicionaCliente(cliente:Cliente):void{
        cliente.id = ClienteServico.buscaClientes().length + 1
        ClienteServico.clientes.push(cliente)
    }

    public static alteraCliente(cliente:Cliente):void{
        for(let i=0; i<ClienteServico.clientes.length; i++){
            let clienteDb = ClienteServico.clientes[i]
            if(clienteDb.id == cliente.id){
                clienteDb = {
                    ...cliente
                }
                break
            }
        }
    }

    public static excluirCliente(cliente:Cliente):void{
        let listaNova = []
        for(let i=0; i<ClienteServico.clientes.length; i++){
            let clienteDb = ClienteServico.clientes[i]
            if(clienteDb.id != cliente.id){
                listaNova.push(clienteDb)
            }
        }

        ClienteServico.clientes = listaNova
    }
}