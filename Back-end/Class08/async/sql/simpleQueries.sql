-- LISTANDO VALORES DAS TABELAS (SIMPLE QUERIES)
-- listando todos os produtos
SELECT * FROM produto;

-- buscar produto por alguma palavra-chave
SELECT * FROM produto WHERE nome LIKE "%USB%";

-- listando todos os pedidos
SELECT * FROM pedido;

-- calculando o total faturado geral (sem critério algum)
SELECT FORMAT(SUM(preco_final), 2, 'pt_BR') as 'total_faturamento (R$)' FROM item_pedido;

-- quantidade de clientes que possuo
SELECT COUNT(id) FROM cliente;

-- mesma consulta acima, mudando o título da consulta
SELECT COUNT(id) as 'total de clientes' FROM cliente;

-- selecionando algumas colunas
SELECT id, nome, cpf, senha FROM cliente;

-- selecionando todas as colunas
SELECT * FROM cliente;

-- buscando através de um critério
SELECT * FROM cliente WHERE id = 1;

-- ordenando clientes pelo nome
SELECT * FROM cliente ORDER BY nome ASC;