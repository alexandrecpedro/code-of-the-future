# Sistema de Cadastro de Produto

## Tecnologias Utilizadas

- **DotNet Core - Template Razor**
- **MySql**

## Desafio

// public readonly string? conexao = "Server=server;Database=database;User Id=user;Password=pass";

- [Exercício Crud Produto - Código do Futuro](https://wordpad.cc/codigo-do-futuro-)
Crie uma aplicação com acesso a banco de dados.
Esta aplicação tem o objetivo de fazer um CRUD (Create, Read, Update, Delete) de produtos
Os dados do modelo serão

id
nome
descricao
data_criacao
data_validade
quantidade_estoque

Vcs irão fazer o mesmo utilizando renderização via server site com Razor. 
https://github.com/torneseumprogramador/persistencia_sql_codigo_do_futuro/tree/main/web

Após o CRUD, criar algumas estatísticas para auxiliar o usuário na home page

Quantidade de itens em estoque
Quantidade de produtos para vencer daqui 3 dias
Quantidade de produtos vencidos
Quantidade de produtos totais

Este itens da home precisam ter links mostrando a lista.

Utilizem para trabalhar com banco de dados o MySql.Data package no nuget
https://www.nuget.org/packages/MySql.Data

- `dotnet watch run`