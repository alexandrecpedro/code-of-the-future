-- para entrar no mysql terminal
mysql -uroot -p'root'

CREATE DATABASE persistence_code_of_the_future;

CREATE TABLE Clientes (
    id int NOT NULL AUTO_INCREMENT,
    nome varchar(100) NOT NULL,
    email varchar(150),
    PRIMARY KEY (id)
);

select * from clientes;

insert into clientes(nome, email) values ('Daniela', 'dani@gmail.com');
insert into clientes(nome, email) values ('Bia', 'bia@gmail.com');
insert into clientes(nome, email) values ('Sung Ju', 'sung@gmail.com');

select * from clientes where id = 2; -- filtra por id
select * from clientes where id in (1,2); -- filtra por múltiplos ids

select * from clientes where nome = 'Daniela'; -- filtra por nome de forma exata
select * from clientes where nome like 'Dan%'; -- filtra por parte do nome no começo
select * from clientes where nome like '%ela'; -- filtra por parte do nome no final
select * from clientes where nome like '%nie%'; -- filtra por parte do texto

select * from clientes 
where id = '10' or email like '%bea%';

update clientes set nome='Beatriz', email='beatriz@gmail.com' where id = 2;

delete from clientes where id = 4;

START TRANSACTION; -- starta uma transação segura onde precisa de confirmação para efetivar
COMMIT; -- confirma a transação
ROLLBACK; -- desfaz a transação

-- como fazer um dump do banco de dados MySQL
mysqldump -u root -p 'root' persistence_code_of_the_future > console/sql/backup.sql
-- como fazer um restore do backup
mysqldump -u root -p 'root' persistence_code_of_the_future < console/sql/backup.sql