# Instalar
https://www.postgresql.org/download/macosx/

# no bashrc, alias para start
```shell
vim ~/.bash_profile

# conteúdo 
alias "postgre_start= postgres -D /usr/local/var/postgres_12"

source ~/.bash_profile
```

# inicia diretório, start service, stop service
```shell
pg_ctl -D /usr/local/var/postgres_12 initdb
pg_ctl -D /usr/local/var/postgres_12 start
pg_ctl -D /usr/local/var/postgres_12 stop
```

# acessar client
```shell
psql
```

# sair do client
```shell
\q
```

# Criar banco de dados
```shell
createdb meu_banco
```

# Criar usuario fora do client psql
```shell
createuser meu_usuario
```

# Acessar banco de dados 
```shell
psql meu_banco
```

# Acessar banco de dados de forma remota
```shell
psql -h localhost -p 5432 -U meu_usuario -d meu_banco
```

# Criar usuario no client psql
```shell
CREATE USER danilo WITH ENCRYPTED PASSWORD '';
```

# altera senha do usuário e dá permissão geral para usuario
```shell
alter user meu_usuario with encrypted password 'minha_senha_123';
grant all privileges on database meu_banco to meu_usuario;
```

# alterando mais permissões para o usuario
```shell
GRANT pg_read_all_data TO meu_usuario;
GRANT pg_write_all_data TO meu_usuario;
GRANT USAGE ON SCHEMA public TO meu_usuario;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO meu_usuario;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO meu_usuario;
GRANT ALL PRIVILEGES ON DATABASE "meu_banco" to meu_usuario;

GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO meu_usuario;
GRANT ALL PRIVILEGES ON DATABASE "meu_banco" to meu_usuario;
ALTER USER meu_usuario WITH SUPERUSER;
```

# Mostrar databases no client psql (show databases)
```shell
\l+ # mostra tabela completa das databaes
SELECT datname FROM pg_database; # mostra nome das databases
```

# use database
```shell
\c meu_banco; 
```

# Mostrar tabelas no client psql (show tables)
```shell
\dt+
```

# Gerar backup
```shell
pg_dump -U meu_usuario -d meu_banco > meubanco_bkp.sql
```

# Restaurar backup
```shell
psql -U danilo -d meu_banco < meubanco_bkp.sql

pg_restore -U meu_usuario -d meu_banco ~/Downloads/meubanco_bkp.sql
```

# Parar serviço postgressql no mac
```shell
# stop
brew services stop postgresql
pg_ctl -D /usr/local/var/postgres stop

# start
pg_ctl -D /usr/local/var/postgres start
```

# Client SGDB
- https://dbeaver.io/download/

# Create table
```sql
CREATE TABLE accounts (
	user_id serial PRIMARY KEY,
	username VARCHAR ( 50 ) UNIQUE NOT NULL,
	password VARCHAR ( 50 ) NOT NULL,
	email VARCHAR ( 255 ) UNIQUE NOT NULL,
	created_on TIMESTAMP NOT NULL,
    last_login TIMESTAMP 
);
```

# Insert data
```sql
insert into accounts (username, "password", email, created_on, last_login)
values ('danilo', '123', 'danilo@teste.com', '2022-04-26 10:00:00', '2022-04-26 10:00:00')
```

# update data
```sql
update accounts set created_on = '2022-04-26 10:10:00'
where user_id = 4
```

# delete data
```sql
delete from accounts where user_id = 4;
```

# data type
- https://www.postgresql.org/docs/current/datatype.html

# alter table
- https://www.postgresql.org/docs/current/sql-altertable.html
```sql
ALTER TABLE distributors ALTER COLUMN name TYPE varchar(100);
```

# select table
```sql
select * from tabela;
```

# select join
```sql
select tabela.* from tabela
inner join tabela2 on tabela2.campo_id = tabela.id;

select tabela.* from tabela
left join tabela2 on tabela2.campo_id = tabela.id;

select tabela.* from tabela
right join tabela2 on tabela2.campo_id = tabela.id;
```

# select order
```sql
select * from tabela order by campo desc
select * from tabela order by campo asc
```

# select group by
- https://www.postgresqltutorial.com/postgresql-tutorial/postgresql-group-by/
```sql
SELECT 
   column_1, 
   column_2,
   sum(column_3) as soma
FROM 
   table_name
GROUP BY 
   column_1,
   column_2
having 
    sum(column_3) > 10
```

# view
- https://www.postgresql.org/docs/9.2/sql-createview.html
```sql
CREATE VIEW comedies AS
    SELECT *
    FROM films
    WHERE kind = 'Comedy';

select * from comedies
```

# procedure
- https://www.postgresql.org/docs/current/sql-createprocedure.html
```sql
CREATE PROCEDURE insert_data(a integer, b integer)
LANGUAGE SQL
AS $$
INSERT INTO tbl VALUES (a);
INSERT INTO tbl VALUES (b);
$$;
```
### or
```sql
CREATE PROCEDURE insert_data(a integer, b integer)
LANGUAGE SQL
BEGIN ATOMIC
  INSERT INTO tbl VALUES (a);
  INSERT INTO tbl VALUES (b);
END;
```
### chamada
```sql
CALL insert_data(1, 2);
```

# function
- https://www.postgresql.org/docs/current/sql-createfunction.html
```sql
CREATE OR REPLACE FUNCTION increment(i integer) RETURNS integer AS $$
BEGIN
    RETURN i + 1;
END;
$$ LANGUAGE plpgsql;
```
### chamada
```sql
select id, nome, increment(tamanho) from usuario;
```