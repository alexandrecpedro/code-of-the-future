export interface PosicoesProduto {
    id: Number
    posicaoX: Number
    posicaoY: Number
    campanha_id: Number,
    produto_id: Number,
    produto: {
        id: Number
        nome: string
        descricao: String
        valor: Number
        qtd_estoque: Number
        categoria_id: Number
        custo:Number
        fotoUrl:String
    }

}