# Instruções
```shell
# rodar códigos abaixo
npm install
npm run dev # para desenvolvimento
# acessar http://localhost:3000/

npm start # para produção
# acessar http://localhost:3000/


# fazendo get todos clientes via terminal
curl http://localhost:3000/clientes

# fazendo get de um cliente via terminal
curl http://localhost:3000/clientes/2

# fazendo post via terminal
curl -d '{"id":"12321", "nome":"Danilo"}' -H "Content-Type: application/json" -X POST http://localhost:3000/clientes

# fazendo delete via terminal
curl -H "Content-Type: application/json" -X DELETE http://localhost:3000/clientes/2

# fazendo put via terminal
curl  -d '{"id":2,"nome":"Leide","cpf":"111111111111","telefone":"123212321","endereco":"endereco teste","valor":2.3}' -H "Content-Type: application/json" -X PUT http://localhost:3000/clientes/2

```