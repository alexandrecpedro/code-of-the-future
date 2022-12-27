-- para entrar no mysql terminal
mysql -uroot -p'root'

CREATE DATABASE IF NOT EXISTS crud_produtos;

USE crud_produtos;

CREATE TABLE Produtos(
    Id varchar(150) NOT NULL PRIMARY KEY,
    Nome varchar(50) NOT NULL,
    Descricao varchar(150) NOT NULL,
    DataCriacao datetime NOT NULL,
    DataValidade datetime NOT NULL,
    QuantidadeEstoque int NOT NULL check(QuantidadeEstoque >= 0)
);