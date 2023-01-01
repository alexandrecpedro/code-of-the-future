# downnload mongo e descompactar
curl https://fastdl.mongodb.org/osx/mongodb-macos-x86_64-5.0.6.tgz

# Mac
https://docs.mongodb.org/manual/tutorial/install-mongodb-on-os-x/

# Linux (ubuntu)
https://docs.mongodb.org/manual/tutorial/install-mongodb-on-ubuntu/

# Windows
https://docs.mongodb.org/manual/tutorial/install-mongodb-on-windows/


# no bashrc
alias "start_mongodb= ~/mongodb/bin/mongod --dbpath ~/mongodb/db/"
alias "mongo= ~/mongodb/bin/mongo"
alias "mongorestore= /Users/danilo/mongodb/bin/mongorestore"
alias "mongodump= /Users/danilo/mongodb/bin/mongodump"

# https://www.mongodb.com/pt-br/basics/create-database
show dbs

# Para criar um banco de dados você usa o comando use. Se o banco de dados não existir, então o cluster do MongoDB irá criá-lo.
use myshinynewdb


# criar coleção

db.createCollection("alunos")

# insert
db.alunos.insert({
    "nome" : "Danilo", 
    "email": "danilo@teste.com",
    "data_nascimento" : new Date (1983,12,09)
})


db.alunos.insert({
    "nome": "Felipe",
    "data_nascimento": new Date(1994, 02, 26),
    "curso": {
        "nome": "Sistemas de informação"
    },
    "notas": [10.0, 9.0, 4.5],
    "habilidades": [{
        "nome": "inglês",
        "nível": "avançado"
    }, {
        "nome": "taekwondo",
        "nível": "básico"
    }]
})


db.alunos.insert(
{"nome" : "Julio",
"data_nascimento" : new Date(1972, 08, 30),
"curso" : {
    "nome" : "Medicina"
},
"habilidades" : [
        {
        "nome" : "inglês",
        "nível" : "avançado"
        }    
    ]
}
)


# busca
db.alunos.find()

# remover
db.alunos.remove({
    "_id" : ObjectId ("56cb0002b6d75ec12f75d3b5")
})


# busca

db.alunos.find(
    {
        nome : "Danilo"
    }
)

db.alunos.find(
    {
        nome : "Danilo"
    }
).pretty()

# Buscando com dado relacional
db.alunos.find({ "habilidades.nome" : "inglês" }).pretty()


# busca com and
db.pedidos.find( { $and: [{nome : "Danilo"}, {valor_total: 10.99}] } )

# buscar com or
db.alunos.find({
    $or : [
        {"curso.nome" : "Sistemas de informação"},
        {"curso.nome" : "Engenharia Química"}    
    ]
})

# busca com or e and
db.alunos.find({
    $or : [
        {"curso.nome" : "Sistemas de informação"},
        {"curso.nome" : "Engenharia Química"}    
    ],
    "nome" : "Daniela"
})

 db.alunos.find({
     $or : [
        {"curso.nome" : "Sistemas de informação"},
        {"curso.nome" : "Engenharia Química"},
        {"curso.nome" : "Moda"}
    ],
    "nome" : "Daniela"
 })

 # busca com in

 db.alunos.find({
    "curso.nome" : {
        $in : ["Sistema de informação", "Engenharia Química"]
        }
})


# Neste filtro retornamos uma coleção por parte do nome (REGEX):
db.pedidos.find({nome: /nilo/})

# insert para teste
db.alunos.insert({
    nome : "Fernando",
    data_nascimento : new Date(1994, 03, 26),
    notas : [ 10, 4.5, 7],
    curso : {
        nome : "Sistema de informação"
    }
})

# busca por nome do curso
db.alunos.find({"curso.nome" : "Sistema de informação"})

# find e update geral, errado
db.alunos.update(
    {"curso.nome" : "Sistema de informação"},
    {
        "curso.nome" : "Sistemas de informação"
    }
)

# find e update somente do item, forma correta, somente o primeiro
db.alunos.update(
    {"curso.nome" : "Sistemas de informação"},
    {
        $set : {
            "curso.nome" : "Sistemas de Informação"
        }
    }
)

# find e update somente do item, forma correta, todos os itens
db.alunos.update(
    {"curso.nome" : "Sistemas de informação"},
    {
        $set : 
           {"curso.nome" : "Sistemas de Informação"}  
        }, 
      {
        multi : true 
      }
)

# find e update somente do item, forma correta, todos os itens, array
db.alunos.update(
    {"_id" : ObjectId("56cb0002b6d75ec12f75d3b5")},
    {$set : {
        {
            "notas" : [
                10,
                9,
                4.5,
                8.5
            ]
        }    
    }
}

# adicionando uma nova nota
db.alunos.update(
    {"_id" : ObjectId("56cb0002b6d75ec12f75d3b5")},
    {
        $push : {
            notas : 8.5
        }    
    }
)

# push de multiplas notas
db.alunos.update(
    {"_id" : ObejctId("56cb0139b6d75ec12f75d3b6")},
    {
        $push : {
            "notas" : {$each : [8.5, 3] }
        }
    }
)

# buscar alunos com nota x
db.alunos.find(
    {
    "notas" : 8.5
})

# usar busca com notas maiores que 5
db.alunos.find({
    notas : { $gt : 5 }
})


# teste de insert para busca
db.alunos.insert({
    nome : "André",
    data_nascimento : new Date(1991,01,25),
    curso : {
        nome : "Matemática"
        },
        notas : [ 7, 5, 9, 4.5 ]
})

db.alunos.insert({
    nome : "Lúcia",
    data_nascimento : new Date(1984,07,17),
    curso : {
        nome : "Matemática"
        },
        notas : [ 8, 9.5,  10 ]
})


# buscando 1 com nota maior que 5
db.alunos.findOne({
    notas : { $gt : 5}
})

# ordem crescente
db.alunos.find().sort({"nome" : 1})

# ordem decrescente
db.alunos.find().sort({"nome" : -1})

# limit 3
db.alunos.find().sort({"nome" : 1}).limit(3)


# join
https://docs.mongodb.com/manual/reference/operator/aggregation/lookup/
### insert
db.usuarios.insert( { nome: "Danilo", telefone: "(11)1111-1111", email: "danilo@tornesemprogramador.com.br" } );
db.comentarios.insert({ usuario_id: db.usuarios.findOne({ "nome": "Danilo" })._id, texto: "Nossa que bacana fazer join com mongo" });
db.usuarios.insert( { nome: "sheila", telefone: "(11)1111-1111", email: "sheila@tornesemprogramador.com.br" } );
db.comentarios.insert({ usuario_id: db.usuarios.findOne({ "nome": "sheila" })._id, texto: "Nossa que bacana fazer join com mongo" });
### find com join
db.usuarios.aggregate({ $lookup:{ from:"comentarios", localField:"_id", foreignField:"usuario_id", as:"comentarios" } }).toArray()


db.LeftTable.aggregate([ # connect all tables {"$lookup": { "from": "RightTable", "localField": "ID", "foreignField": "ID", "as": "R" }}, {"$unwind": "R"} ])
db.LeftTable.aggregate([ # connect all tables {"$lookup": { "from": "RightTable", "localField": "ID", "foreignField": "ID", "as": "R" }}, {"$unwind": "R"}, # define conditionals + variables {"$project": { "midEq": {"$eq": ["$MID", "$R.MID"]}, "ID": 1, "MOB": 1, "MID": 1 }} ])


# join com new doc resumido
db.authors.insert([ { _id: 'a1', name: { first: 'orlando', last: 'becerra' }, age: 27 }, { _id: 'a2', name: { first: 'mayra', last: 'sanchez' }, age: 21 } ]);
db.categories.insert([ { _id: 'c1', name: 'sci-fi' }, { _id: 'c2', name: 'romance' } ]);
db.books.insert([ { _id: 'b1', name: 'Groovy Book', category: 'c1', authors: ['a1'] }, { _id: 'b2', name: 'Java Book', category: 'c2', authors: ['a1','a2'] }, ]);
db.lendings.insert([ { _id: 'l1', book: 'b1', date: new Date('01/01/11'), lendingBy: 'jose' }, { _id: 'l2', book: 'b1', date: new Date('02/02/12'), lendingBy: 'maria' } ]);
db.books.().forEach( function (newBook) { newBook.category = db.categories.findOne( { "_id": newBook.category } ); newBook.lendings = db.lendings.find( { "book": newBook._id } ).toArray(); newBook.authors = db.authors.find( { "_id": { $in: newBook.authors } } ).toArray(); db.booksReloaded.insert(newBook); } );
db.booksReloaded.find().pretty()



# busca por proximidade
db.alunos.update(
{ "_id" : ObjectId("56cb0139b6d75ec12f75d3b6") },
{
    $set : {
    localizacao : {
        endereco : "Rua Vergueiro, 3185",
        cidade : "São Paulo",
        coordinates : [-23.588213, -46.632356],
        type : "Point"
        }
    }
}
)

# criando indice
db.alunos.aggregate([
{
    $geoNear : {
        near : {
            coordinates: [-23.5640265, -46.6527128],
            type : "Point"
        }

    }
}
])

db.alunos.createIndex({
    localizacao : "2dsphere"
})

# acregando indice
db.alunos.aggregate([
{
    $geoNear : {
        near : {
            coordinates: [-23.5640265, -46.6527128],
            type : "Point"
        },
        distanceField : "distancia.calculada",
        spherical : true
    }
}
])

db.alunos.createIndex({
    localizacao : "2dsphere"
})

# acregando indice
db.alunos.aggregate([
{
    $geoNear : {
        near : {
            coordinates: [-23.5640265, -46.6527128],
            type : "Point"
        },
        distanceField : "distancia.calculada",
        spherical : true,
        num : 4
    }
},
{ $skip :1 }
])