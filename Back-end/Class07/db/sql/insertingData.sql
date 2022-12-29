-- INSERINDO VALORES NAS TABELAS
-- tabela departamento
INSERT INTO departamento VALUES (null, "Tecnologia", "Equipamentos, computadores, tablets");
INSERT INTO departamento VALUES (null, "Acessorios", "Cabos, adaptadores, carregadores");
INSERT INTO departamento VALUES (null, "Games", "Jogos, Consoles, Acessórios");

-- tabela produto
INSERT INTO produto VALUES (null, "Computador Ultima Geração","Computaodr i9 32Gb Ram 1TbHD, Placa GTX1090",4000.0,3,"computador.jpg",1);
INSERT INTO produto VALUES (null, "Notebook Ultima Geração","Notebook i7 16 Gb Ram 500Gb HD SSD",3850.00,5,"notebook.jpg",1);
INSERT INTO produto VALUES (null, "Cabo USB C","Cabo USB 2M para carregar celulares",50.0,20,"cabousb.jpg",2);
INSERT INTO produto VALUES (null, "Conector USB","Conector USB para recarga em tomada 3A",35,15,"conector.jpg",2);
INSERT INTO produto VALUES (null, "Console XBOX One","Console XBOX One com Halo e 2 controles",1999.90,3,"xbox.jpg",3);
INSERT INTO produto VALUES (null, "Console Playstation 5","Nao acompanha jogo, vem com 1 controle",2999.90,2,"ps5.jpg",3);

-- tabela cliente
INSERT INTO cliente VALUES (null, "Jose Alberto Neves","josealberto@mail.com","123456","987.654.321-00");
INSERT INTO cliente VALUES (null, "Antonio Oliveira","antoniooli@mail.com","987654","765.432.987.10");
INSERT INTO cliente VALUES (null, "Regina Brito","reginabrito@mail.com","010203","123.456.789-00");
-- INSERT INTO cliente VALUES (null, "Jose Alberto Neves","josealberto@mail.com","123456","98.876.123-99","987.654.321-00");
-- INSERT INTO cliente VALUES (null, "Antonio Oliveira","antoniooli@mail.com","987654","12.983.256-72","765.432.987.10");
-- INSERT INTO cliente VALUES (null, "Regina Brito","reginabrito@mail.com","010203","83.235.645-90","123.456.789-00");

-- tabela endereco
INSERT INTO endereco VALUES (null,"Av", "Brasil",100,"","Centro","Sao Paulo","SP","01234-567",1);
INSERT INTO endereco VALUES (null,"Rua","Major Silva",12,"Ap 12A","Pitangueiras","Itapecerica da Serra","SP","98765-432",1);
INSERT INTO endereco VALUES (null,"Rua","Heitor Vila Lobos",98,"Casa 2","Vila das Flores","Osasco","SP","06543-123",2);
INSERT INTO endereco VALUES (null,"Av","Raquel Meyer",173,"Ap 42 Bl 1","Centro","Vitoria","ES","32987-122",3);

-- tabela pedido
INSERT INTO pedido(numero, data_pedido, valor_bruto, status,desconto,valor_liq,id_cliente) 
VALUES 
    (null,"2021-01-10",4000,"F",0,4000,1),
    (null,"2021-02-01",70,"F",0,70,2),
    (null,"2021-03-10",3850,"F",0,3850,3),
    (null,"2021-04-15",8000,"F",1000,7000,1),
    (null,"2021-05-05",1999.90,"F",0,1999.90,2),
    (null,"2021-06-08",2999.90,"F",0,2999.90,3),
    (null,"2021-07-10",250,"F",0,250,2),
    (null,"2021-08-22",350,"F",0,350,2),
    (null,"2021-09-19",700,"F",0,700,3),
    (null,"2021-10-07",3850,"F",0,3850,1),
    (null,"2021-11-01",50,"F",0,50,1),
    (null,"2021-12-05",1999.9,"F",0,1999.9,1),
    (null,"2022-12-05",2999.9,"F",0,2999.9,2),
    (null,"2022-12-12",4000,"F",0,4000,3);

-- tabela itens
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 1, 1, 1, 4000, 4000);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 2, 4, 2, 35.0, 70.0);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 3, 2, 1, 3850, 3850);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 4, 1, 2, 4000, 8000);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 5, 5, 1, 1999.90, 1999.90);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 6, 6, 1, 2999.90, 2999.90);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 7, 3, 5, 50, 250);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 8, 4, 10, 35, 350);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 9, 4, 20, 35, 700);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 10, 2, 1, 3850, 3850);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 11, 3, 1, 50, 50);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 12, 5, 1, 1999.9, 1999.9);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 13, 6, 1, 2999.9, 2999.9);
INSERT INTO item_pedido(seq, numero_pedido, codigo_produto, quantidade, preco_final, preco_unit) VALUES (null, 14, 1, 1, 4000, 4000);