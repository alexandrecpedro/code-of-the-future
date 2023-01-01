# Caso 1

    - Da empresa que tem R$ 100 mil para fazer um software
    - Ela deseja que o sistema tenha por volta de 1000 usuários simutâneos
        porém a realidade é que ela não passa de 400 usuários, pelo menos nos
        seus primeiros 6 meses
    - Multiplos programadores ou será um grade empregador, porem na realidade
        você e mais 2 pessoas são a parte de tecnologia
    - Quando ele vê que os R$ 100 mil está acabanco começa a colocar pressão nos devs

    Distribuição entre equipe (Mês)
        - Vendas ativas R$ 5000 = R$ 1000 
        - Marketing R$ 1800
        - Trafego R$ 3500
        - Programadores R$ 5000 * 3
        - R$ 6000 prolabori (dono sobreviver no negócio)
        - R$ 2000 - agua, luz, aluguel
        - Manutenção do sistema (Nuvem)
            Maquina R$ 244
            IP fixo R$ 1.90
            Dominio R$ 7 dominio
            Banco de dados R$ 801
    Resumo mes:
        - 29300 + 1100 = R$ 30400

Estratégia 0 - Tem pouco dinheiro
App WEB MVC
    - sistema - Servidores: 1 = R$ 500

    - Fluter - App (instalado no celular do cliente)

Estratégia 1
App WEB MVC
    - sistema
    - Servidores: 2 = R$ 1000
    - fila - kafka - Servidores: 1 = R$ 277

    - Fluter - App (instalado no celular do cliente)

Estratégia 2
API
    - sistema (db) - Servidores: 2 = R$ 1000
    - fila - kafka - Servidores: 1 = R$ 277

Front-End
    - React - Servidores: 1 = R$ 277
    - Fluter - App (instalado no celular do cliente)

Estratégia 3 - É para grandes orçamentos e com times separados para cada API
API
    - usuarios (db) - Servidores: 2 = R$ 555
    - produtos (db) - Servidores: 2 = R$ 555
    - pedidos (db) - Servidores: 2 = R$ 555
    - fila - kafka - Servidores: 1 = R$ 277

Front-End
    - React - Servidores: 1 = R$ 277
    - Fluter - App (instalado no celular do cliente)

jmeter - testa audiencia

Olhando somente para o servidor de Banco de dados
quando que eu sei se utilizo banco de dados (Normalizado) ou (não normalizado)

velicidade R$
volume R$

Qual é a prioridade da sua empresa?
Economia ou velocidade ?

economia orçamento de R$ 1000
NAO VAI UTILIZAR BANCO DE DADOS NÃO NORMALIZADO (mongodb, cassandra, couchdb, hbase, redis, firebase)
UTILIZE BANCO DE DADOS NORMALIZADO (Mysql, Postgresql, Oracla, Sql server ...) - o peso do banco é menor para backup e tamanho em disco

Velocidade - banco de dados não normalizado
Econimia - banco de dados normalizado

NÃO NORMALIZAO - A repetição para ganho de velocidade é válida
NORMALIZAO - A repetição é seu inimigo pois aumenta o tamanho dos dados.
