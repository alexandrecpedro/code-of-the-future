# Sistema de Gerenciamento de Locação de Carros

## Desafio

*Danilo professor da Gama Academy precisa de um sistema de gerenciamento de locação de veiculos
faça um programa que cadastre através de cruds os seguintes modelos:*

- Modelo(id, nome)
- Marca(id, nome)
- Carro(id, nome, marca, modelo)
- Cliente(id, nome, email, telefone, endereco)
- Pedido(id, idcliente, carro, dataLocacao, dataEntrega)
- Configuracao(id, diasDeLocacao)

[Danilo Aparecido - Torne se um Programador](https://www.torneseumprogramador.com.br/)

## Solução

#### Tecnologias Utilizadas

- **DotNet Core - Template MVC**

- **MySql**
  - Utilização do pacotes do Entity Framework - detalhes em referências.
  - Modelo de string de conexão MySql (ver appsettings.json)
```c#
"ConnectionStrings": {
    "conexao": "Server=localhost;Database=database;Uid=user;Pwd=password;"
  }
```
   - Migrations

##### Porque utilizar o MySQL

- Baixo orçamento
- Volume de dados (espaço disponível em disco) é mais relevante do que a velocidade de requisições para este contexto;

#### Referências

- [Repositório GITHUB do Torne-se um Programador - Código do Futuro Entity](
https://github.com/torneseumprogramador/codigo-do-futuro-entity)
- [Exercício Banco de Dados [Migrations e Scaffold] - Código do Futuro](https://wordpad.cc/codigo-do-futuro)
- [Nuget - Package ENTITY FRAMEWORK CORE](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
- [Nuget - Package ENTITY FRAMEWORK SQL SERVER](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)
- [Nuget - Package ENTITY FRAMEWORK TOOLS](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools)
- [Nuget - Package POMELO ENTITY FRAMEWORK MySQL](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/7.0.0-silver.1)
- [Nuget - Package WEB CODE GENERATION DESIGN](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design)
- [Orçamento](Assets/Or%C3%A7amento.md)
- [Orçamento - Danilo Aparecido](https://wordpad.cc/introducao-a-banco-de-dados-voltado-ao-negocio)
