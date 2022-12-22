### DESAFIO
Crie uma aplicação console onde terá um cadastro de pessoas no geral
este cadastro de pessoas vai armazenar pessoas fisicas ou juridicas

JSON com pessoa, fisica ou juridica

Tem que ter uma tela de menu, onde vc escolhe a opção de cadastro, se for pessoa fisica, utilizará o modelo de pessoa fisica, se juridica
utilizará o modelo de pessoa juridica

porem o serviço para gravar o json, salva no mesmo local, independente se é fisica ou juridica, gravando um json assim

pessoas.json
[
  { Id: 1,  Nome: 'Gustavo', Documento: '333.333.333-99', Tipo: 'F'},
  { Id: 1,  Nome: 'Portes e Transportes LTDA', Documento: '00.000.000/0000-00', Tipo: 'J'}
]

Voce terá uma classe Usuario com as propriedades
Id:  Nome, CPF

Voce terá uma classe Fornecedor com as propriedades
Id:  Nome, CNPJ

Inclusão de dados

Leitura da tabela na tela de um app console

escreve arquivo: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
ler arquivo: https://www.educative.io/answers/how-to-read-a-text-file-in-c-sharp

serialize e deserialize json c# : https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0