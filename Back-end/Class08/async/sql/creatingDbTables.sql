-- ENTRAR NO MySQL TERMINAL
mysql -uroot -p'root'

-- MOSTRAR STATUS
status;

-- LIMPAR TELA
\! clear; -- MacOS ou Linux
-- \! cls; -- Windows

-- MOSTRAR BASE DE DADOS EXISTENTES
SHOW DATABASES;

-- CRIAR BASE DE DADOS
CREATE DATABASE ecommerce;

-- USAR BASE DE DADOS
USE ecommerce;

-- MOSTRAR TABELAS EXISTENTES NA BASE DE DADOS SELECIONADA
SHOW TABLES;

-- CRIAR TABELAS
CREATE TABLE cliente (
    id              INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome            VARCHAR(100) NOT NULL,
    email           VARCHAR(70) NOT NULL UNIQUE,
    senha           VARCHAR(20) NOT NULL,
    cpf             VARCHAR(15) NOT NULL UNIQUE
);

CREATE TABLE departamento (
    numero          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome            VARCHAR(30) NOT NULL,
    descricao       TEXT
);

CREATE TABLE endereco (
    num_seg         INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    tipo            VARCHAR(5) NOT NULL,
    logradouro      VARCHAR(50) NOT NULL,
    numero          VARCHAR(10),
    compl           VARCHAR(20),
    bairro          VARCHAR(30) NOT NULL,
    cidade          VARCHAR(50) NOT NULL,
    estado          CHAR(2) NOT NULL,
    cep             VARCHAR(10) NOT NULL,
    id_cliente      INTEGER NOT NULL,
    CONSTRAINT endereco_cliente FOREIGN KEY (id_cliente) REFERENCES cliente(id)
);

CREATE TABLE pedido (
    numero          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    status          VARCHAR(1) NOT NULL,
    data_pedido     DATE,
    valor_bruto     DOUBLE,
    desconto        DOUBLE,
    valor_liq       DOUBLE,
    id_cliente      INTEGER NOT NULL,
    CONSTRAINT cliente_pedido FOREIGN KEY (id_cliente) REFERENCES cliente(id)
);

CREATE TABLE produto (
    codigo          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome            VARCHAR(50) NOT NULL,
    descricao       TEXT,
    preco           DOUBLE,
    estoque         INTEGER,
    link_foto       VARCHAR(255),
    numero_depto    INTEGER NOT NULL,
    CONSTRAINT produto_depto FOREIGN KEY (numero_depto) REFERENCES departamento(numero)
);

CREATE TABLE item_pedido (
    seq             INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
    quantidade      INTEGER,
    preco_unit      DOUBLE,
    preco_final      DOUBLE,
    codigo_produto  INTEGER NOT NULL,
    numero_pedido   INTEGER NOT NULL,
    CONSTRAINT item_produto FOREIGN KEY (codigo_produto) REFERENCES produto(codigo),
    CONSTRAINT item_pedido FOREIGN KEY (numero_pedido) REFERENCES pedido(numero)
);

-- MOSTRAR ESTRUTURA DE UMA TABELA
-- DESCRIBE cliente;
DESC cliente;

-- ALTERANDO ESTRUTURAS DE UMA TABELA
-- ALTER TABLE cliente ADD COLUMN rg VARCHAR(10) NOT NULL AFTER senha; -- adiciona uma coluna na tabela cliente
-- ALTER TABLE cliente MODIFY COLUMN rg VARCHAR(15); -- modifica a estrutura da coluna rg da tabela cliente
-- ALTER TABLE cliente CHANGE COLUMN rg registro_geral VARCHAR(10) NOT NULL; -- modifica a estrutura e o nome da coluna rg da tabela cliente
-- ALTER TABLE cliente DROP COLUMN registro_geral; -- remove a coluna registro_geral da tabela cliente

-- REMOVENDO TABELA E DATABASE
-- DROP TABLE cliente; -- remove tabela cliente
-- DROP DATABASE ecommerce; -- remove base de dados ecommerce