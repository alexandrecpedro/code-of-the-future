export interface Estado {
    id: Number
    sigla: String 
    nome: String 
    regiao: {id: Number,
    sigla: String,
    nome:String}
}