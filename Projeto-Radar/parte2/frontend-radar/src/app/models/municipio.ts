export interface Municipio {
    id: Number
    nome: String 
    microrregiao: {
        id: Number
        nome: String 
        mesorregiao:{
            id: Number
            nome: String 
            UF:{
                id: Number
                nome: String
                regiao: {id: Number,
                    sigla: String,
                    nome:String} 
            }
        }
    }
    "regiao-imediata": {
        id: Number,
        nome: String,
        "regiao-intermediaria": {
          id: Number,
          nome: String,
          UF: {
            id: Number,
            sigla: String,
            nome: String,
            regiao: {
              id: Number,
              sigla: String,
              nome: String
            }
          }
        }
      }
}
