# INTRODUÇÃO A BANCO DE DADOS VOLTADO AO NEGÓCIO - PARTE 1

## Caso 1
    - Da empresa que tem R$ 100 mil para fazer um software
    - ela deseja que o sistema tenha por volta de 100 usuários simultâneos
        porém a realidade é que ela não passa de 400 usuários, pelo menos nos
        seus primeiros 6 meses
    - Múltiplos programadores ou será um grande empregador, porém na realidade
        você e mais 2 pessoas são a parte de tecnologia
    - Quando ele vê que os R$ 100 mil está acabando começa a colocar pressão nos devs

### Distribuição entre a equipe (Custos Mês)
    - Vendas ativas R$ 5000 = R$ 1000
    - Marketing R$ 1800
    - Tráfego R$ 3500
    - Programadores R$ 15000 (R$ 5000 cada um)
    - Pro-labore R$ 6000 (dono sobreviver no negócio)
    - Despesas fixas (água, luz, aluguel) R$ 2000
    - Manutenção do sistema (Nuvem)
        - Máquina = (US$ 0,064/h)*(24h/1 dia)*(R$5,30/US$)*(30 dias/mes) = R$ 244 / mês
        - IP fixo = (US$ 0,0005/h)*(24h/1 dia)*(R$5,30/US$)*(30 dias/mes) = R$ 1,90 / mes
        - Dominio = (R$ 40/ano) * (1 ano/6 meses) = R$ 7
        - Banco de Dados = (US$ 0,21/h)*(24h/1 dia)*(R$5,30/US$)*(30 dias/mes) = R$ 801

### Resumo mês:
    - R$ 30400
    
### Duração:
    - empresa não dura 3 meses (faltam outros custos a serem adicionados)
<hr>

```
Estratégia 0 - Tem pouco dinheiro
Única VM (sem escala)
    - sistema (db) => Servidores: 1 (aplicacao + bd) = R$ 500

Front-End
    - Flutter => App (instalado no celular do cliente)
```
<hr>

```
Estratégia 1
API
    - sistema (db) => Servidores: 2 (1 aplicacao + 1 bd) = R$ 1000
    - fila - kafka => Servidores: 1 = R$ 277

Front-End
    - Flutter => App (instalado no celular do cliente)
```
<hr>

```
Estratégia 2
API
    - sistema (db) => Servidores: 2 (1 aplicacao + 1 bd) = R$ 1000
    - fila - kafka => Servidores: 1 = R$ 277

Front-End
    - React => Servidores: 1 = R$ 277
    - Flutter => App (instalado no celular do cliente)
```
<hr>

```
Estratégia 3 - É para grandes orçamentos e com times separados para cada API
API
    - usuarios (db) => Servidores: 2 (1 aplicacao + 1 bd) = R$ 555
    - produtos (db) => Servidores: 2 (1 aplicacao + 1 bd) = R$ 555
    - pedidos (db) => Servidores: 2 (1 aplicacao + 1 bd) = R$ 555
    - fila - kafka => Servidores: 1 = R$ 277

Front-End
    - React => Servidores: 1 = R$ 277
    - Flutter => App (instalado no celular do cliente)
```

jmeter - testa audiência