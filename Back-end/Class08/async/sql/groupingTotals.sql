-- AGRUPAMENTOS E TOTALIZAÇÕES
-- como saber quantos produtos há em cada departamento
SELECT * FROM produto;

-- quantos produtos há em cada um dos departamentos
SELECT numero_depto, COUNT(codigo) as 'quantidade de produtos' FROM produto
    GROUP BY numero_depto;

-- somatória dos produtos por departamento
SELECT numero_depto, FORMAT(SUM(preco*estoque), 2, 'pt_BR') as 'Valor (R$)' FROM produto
    GROUP BY numero_depto;