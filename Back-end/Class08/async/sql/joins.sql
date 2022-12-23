/* JUNÇÕES DE DUAS TABELAS */
-- junção simples como produto cartesiano
SELECT * FROM produto INNER JOIN departamento;

-- junção fazendo a relação entre produto e departamento
SELECT * FROM produto INNER JOIN departamento
    ON produto.numero_depto = departamento.numero;

-- listar todos os clientes e seus respectivos endereços
SELECT * FROM cliente INNER JOIN endereco
    ON endereco.id_cliente = cliente.id;

/* JUNÇÕES DE TRÊS OU MAIS TABELAS */
-- passo 1  a partir dos pedidos, faço a junção com os itens de pedido
-- passo 2: fazer a junçao com produto
-- passo 3: fazer a junção com cliente
select * from pedido 
    inner join item_pedido on pedido.numero = item_pedido.numero_pedido
	inner join produto on produto.codigo = item_pedido.codigo_produto
    inner join cliente on pedido.id_cliente = cliente.id;
    
-- a mesma consulta anterior, porém buscando todos os dados do pedido + nome
-- do cliente + o nome do produto comprado

select pedido.numero, pedido.data_pedido, pedido.valor_bruto,
       pedido.desconto, pedido.valor_liq, cliente.nome as 'Cliente',
       produto.nome as 'Produto' from 
     pedido inner join item_pedido 
		on pedido.numero = item_pedido.numero_pedido
	inner join produto on produto.codigo = item_pedido.codigo_produto
    inner join cliente on pedido.id_cliente = cliente.id
    order by pedido.numero;

-- quero totais de pedidos por cliente (inclusive com o nome deles)
select cliente.nome, pedido.id_cliente, format(sum(pedido.valor_liq), 2, 'pt_BR') as 'Valor (R$)'
    from pedido inner join cliente on pedido.id_cliente = cliente.id
    group by pedido.id_cliente;

/* JUNÇÕES EXTERNAS */
-- hipótese 1: buscando todos os produtos a partir dos departamentos
select * from departamento inner join produto on
	departamento.numero = produto.numero_depto;
    
-- inserindo um novo departamento
insert into departamento values (null, "Moveis", "Moveis para escritórios e gamers de todas as idades");

select * from departamento;

-- solução para isso: usar uma junção externa (outer join)
select * from departamento left outer join produto
	on departamento.numero = produto.numero_depto;
    
-- agora usando right outer join
select * from produto right outer join departamento
	on departamento.numero = produto.numero_depto;