import { HttpClient } from '@angular/common/http';
import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Pedido } from 'src/app/models/pedido';
import { Categoria } from 'src/app/models/categoria';
import { PedidoServico } from 'src/app/servicos/pedidoServico';
import { CategoriaServico } from 'src/app/servicos/categoriaServico';
import { PedidoProdutoServico } from 'src/app/servicos/pedidoProdutoServico';
import { ProdutoServico } from 'src/app/servicos/produtoServico';
import { PedidoProduto } from 'src/app/models/pedidoProduto';
import { Produto } from 'src/app/models/produto';
import { ChartType } from 'angular-google-charts';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private router: Router,
    private http: HttpClient,
  ) {
  }

  //Variáveis de Serviço CRUD
  public pedidoServico: PedidoServico = {} as PedidoServico;
  public categoriaServico: CategoriaServico = {} as CategoriaServico;
  public pedidoProdutoServico: PedidoProdutoServico = {} as PedidoProdutoServico;
  public produtoServico: ProdutoServico = {} as ProdutoServico;

  //Arrays com dataBinding
  public categoriasMostradas: Categoria[] = [];

  //Arrays Filtrados
  public pedidosProdutosSelecionados: PedidoProduto[] = []
  public produtosSelecionados: Produto[] = [];
  public pedidosSelecionados: Pedido[] = [];

  //Arrays com todos os valores
  private categorias: Categoria[] = []
  private pedidos: Pedido[] = []
  private pedidosProdutos: PedidoProduto[] = []
  private produtos: Produto[] = []

  //Variáveis com dataBinding
  public categoriaSelecionado:String="";
  public dataInicial:String = "01/01/2023";
  public dataFinal:String = String(new Date(Date.now()));
  public dataMaxima:String = String(new Date(Date.now()));
  public valorTotal:Number=0;
  public valorPositivo:Number=0;
  public valorNegativo:Number=0;

  //Gráficos
  titleColum = "asd";
  columChart = ChartType.LineChart;
  dataColum: any[] = [
    [0, 0],   [1, 10],  [2, 23],  [3, 17],  [4, 18],  [5, 9],
    [6, 11],  [7, 27],  [8, 33],  [9, 40],  [10, 32], [11, 35],
    [12, 30], [13, 40], [14, 42], [15, 47], [16, 44], [17, 48],
    [18, 52], [19, 54], [20, 42], [21, 55], [22, 56], [23, 57],
    [24, 60], [25, 50], [26, 52], [27, 51], [28, 49], [29, 53],
    [30, 55], [31, 60], [32, 61], [33, 59], [34, 62], [35, 65],
    [36, 62], [37, 58], [38, 55], [39, 61], [40, 64], [41, 65],
    [42, 63], [43, 66], [44, 67], [45, 69], [46, 69], [47, 70],
    [48, 72], [49, 68], [50, 66], [51, 65], [52, 67], [53, 70],
    [54, 71], [55, 72], [56, 73], [57, 75], [58, 70], [59, 68],
    [60, 64], [61, 60], [62, 65], [63, 67], [64, 68], [65, 69],
    [66, 70], [67, 72], [68, 75], [69, 80]];
  columnsNames: any[] = ['x','dogs'];
  widthColum = 600;
  heightColum = 400;
  optionsColum = {
    width: 1200,
    height: 400,
    legend: { position: 'top', maxLines: 3 },
    bar: { groupWidth: '75%' },
    isStacked: true,
  };

  titleArea = "Performance por Categoria"
  areaChart = ChartType.AreaChart;
  dataArea: any[] = []
  optionsArea = {
    width: 600,
    height: 400,
    hAxis: { titleTextStyle: { color: '#333' } },
    vAxis: { title: "Faturamento", minValue: 0 }
  }

  titlePie = "5 Produtos Mais Vendidos";
  pieChart = ChartType.PieChart;
  dataPie = [
    ['Name1', 5.0],
    ['Name2', 36.8],
    ['Name3', 42.8],
    ['Name4', 18.5],
    ['Name5', 16.2]
  ];
  columnNamesPie = ['Name', 'Percentage'];
  optionsPie = {

    width: 500,
    height: 300
  };

  ngOnInit(): void {
    this.pedidoServico = new PedidoServico(this.http);
    this.categoriaServico = new CategoriaServico(this.http);
    this.pedidoProdutoServico = new PedidoProdutoServico(this.http);
    this.produtoServico = new ProdutoServico(this.http);
    this.listaDeCategorias();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.filtraData();
  }

  private async listaDeCategorias() {
    let categorias = await this.categoriaServico.lista();
    categorias?.forEach(categoria => {
      this.categorias.push(categoria);
    })
    this.categoriasMostradas = this.categorias;
    await this.listaDePedidos();
    await this.listaDeProdutos();
    await this.listaDePedidosProdutos();
    this.gerarGraficoBarra();
    this.gerarGraficoArea(0);
    this.gerarPie()
  }
  private dataBr(data: Date): string {
    return data.getDate().toString() + "/" + (data.getMonth() + 1).toString() + "/" + data.getFullYear().toString()
  }

  private gerarGraficoBarra() {
    let data:any[]=[]
    let title="Faturamento ao Longo do tempo"
    let dictPedido:Map<Number,Date>=new Map()
    this.pedidosSelecionados.forEach(pedido=>{
      dictPedido.set(pedido.id,new Date(pedido.data));
    })
    let dictData:Map<Date,Number>=new Map()
    this.pedidosProdutosSelecionados.forEach(pedidoProduto=>{
      let date=dictPedido.get(pedidoProduto.pedido_id)
      if(date){
        if(dictData.get(date)){
          let datas=dictData.get(date)
          if(datas)data[Number(datas)][1]+=Number(pedidoProduto.quantidade)*Number(pedidoProduto.valor)
        }else{
          data.push([this.dataBr(date),Number(pedidoProduto.quantidade)*Number(pedidoProduto.valor)])
          dictData.set(date,data.length-1);
        }
      }
    })
    for (let i = 1; i < data.length; i++) {
      data[i][1]+=data[i-1][1];
    }
    this.dataColum = data
    this.titleColum = title
  }
  private gerarPie() {
    let dataPie:any[] =[]
    let nomes: string[] = [""]
    let dictProduto: Map<Number, Number> = new Map();
    this.produtosSelecionados.forEach(produto => {
      nomes.push(produto.nome.toString())
      dictProduto.set(produto.id, nomes.length - 1);
    })
    let produtos: any[] = []
    let dictProdutoTemp: Map<Number, Number> = new Map();
    this.pedidosProdutosSelecionados.forEach(pedidoProduto => {
      if (dictProdutoTemp.get(pedidoProduto.produto_id)) {
        let id = Number(dictProdutoTemp.get(pedidoProduto.produto_id))
        if (id) produtos[id][1] += (Number(pedidoProduto.quantidade) * Number(pedidoProduto.valor))
      } else {
        let id = dictProduto.get(pedidoProduto.produto_id)
        if (id){ 
          produtos.push([nomes[Number(id)], Number(pedidoProduto.quantidade) * Number(pedidoProduto.valor)]) 
          dictProdutoTemp.set(pedidoProduto.produto_id, produtos.length-1)
        }
      }
    })
    let vazio:any[]=[]
    for (let i = 0; i < produtos.length; i++) {
      const produto = produtos[i];
      if(produto[1]>0){
        vazio.push(produto)
      }
    }
    produtos=vazio
    if (produtos.length > 4) {
      let index = [0, 0, 0, 0]
      let valor = [0, 0, 0, 0]
      for (let i = 0; i < produtos.length; i++) {
        const produto = produtos[i];
        let val = Number(produto[1]);
        if (valor[0] < val) {
          valor[0] = val
          index[0] = i
          for (let i = 1; i < 4; i++) {
            if (valor[i - 1] > valor[i]) {
              let temp = valor[i]
              valor[i] = valor[i - 1]
              valor[i - 1] = temp
              let tem = index[i]
              index[i] = index[i - 1]
              index[i - 1] = tem
            }
          }
        }
      }
      let outros = 0
      for (let i = 0; i < produtos.length; i++) {
        const produto = produtos[i];
        if (!index.find(ind => ind == i)) {
          outros += Number(produto[1])
        }
      }
      let total = 0
      valor.forEach(val => {
        total += val
      })
      console.log(outros)
      total += outros
      let nome1 = produtos[index[3]][0]
      let nome2 = produtos[index[2]][0]
      let nome3 = produtos[index[1]][0]
      let nome4 = produtos[index[0]][0]
      let real1 = produtos[index[3]][1] / total
      let real2 = produtos[index[2]][1] / total
      let real3 = produtos[index[1]][1] / total
      let real4 = produtos[index[0]][1] / total
      dataPie = [[nome1, real1],
      [nome2, real2],
      [nome3, real3],
      [nome4, real4]]
      let restante = 1 - real1 - real2 - real3 - real4
      dataPie.push(["Outros", restante])
    }else{
      dataPie=produtos
    }
    this.dataPie = dataPie
    console.log("Pie ",this.dataPie)
  }

  private gerarGraficoArea(categoria: Number) {
    let dataInicial = this.corrigirInicial(this.dataInicial);
    let dataFinal = this.aumentar(this.dataFinal);
    let dataArea: any[] = []
    let title = `Performance por Categoria ao longo do tempo`
    let columnsNames:String[]=["data"]
    let datas: any[] = [dataInicial]
    let diferenca = dataFinal.getTime() - dataInicial.getTime()
    let layers = this.categorias.length
    if (!(categoria.toString() === "0")) {
      this.titleArea = `Performance por Produto dentro de ${this.categorias[Number(categoria)]} ao longo do tempo`
      this.produtosSelecionados.forEach(produto=>{
        columnsNames.push(produto.nome)
      })
      layers = this.produtosSelecionados.length
    }else{
      this.categorias.forEach(categoria=>{
        columnsNames.push(categoria.nome)
      })
    }

    let dictPedidoTemp: Map<Number, Number> = new Map();
    for (let i = 0; i < 20; i++) {
      datas.push(new Date(datas[i].getTime() + diferenca / 20))
      dataArea.push([this.getStringArea(dataInicial, dataFinal, new Date(datas[i].getTime() + diferenca / 20))])
      for (let j = 0; j < layers; j++) {
        dataArea[i].push(0)
      }
    }
    this.pedidos.forEach(pedido => {
      let datar = new Date(pedido.data.toString())
      for (let i = 0; i < 20; i++) {
        if (datar > datas[i] && datar <= datas[i + 1]) dictPedidoTemp.set(pedido.id, i)
      }
    });
    if (!(categoria.toString() === "0")) {
      let cont = 0
      this.produtosSelecionados.forEach(produto => {
        cont++
        let id_produto = produto.id;
        this.pedidosProdutos.forEach(pedidoProduto => {
          let id = dictPedidoTemp.get(pedidoProduto.pedido_id)
          if (id && pedidoProduto.produto_id.toString() === id_produto.toString()) {
            dataArea[Number(id)][cont] += Number(pedidoProduto.valor) * Number(pedidoProduto.quantidade)
          }
        })
      })
    } else {
      let cont = 0
      this.categorias.forEach(categoria => {
        cont++
        let dictProdutoTemp: Map<Number, boolean> = new Map()
        let categoria_id = categoria.id
        this.produtosSelecionados.forEach(produto => {
          dictProdutoTemp.set(produto.id, produto.categoria_id.toString() === categoria_id.toString());
        })
        this.pedidosProdutos.forEach(pedidoProduto => {
          let id = dictPedidoTemp.get(pedidoProduto.pedido_id)
          if (id && dictProdutoTemp.get(pedidoProduto.produto_id)) {
            dataArea[Number(id)][cont] += Number(pedidoProduto.valor) * Number(pedidoProduto.quantidade)
          }
        })
      })

    }
    for (let i = 1; i < dataArea.length; i++) {
      for (let j = 1; j < dataArea[i].length; j++) {
        dataArea[i][j] += dataArea[i - 1][j];
      }
    }
    console.log(columnsNames,dataArea)
    this.columnsNames=columnsNames
    this.dataArea = dataArea
    this.titleArea = title
  }

  private getStringArea(dataIni: Date, dataFim: Date, atual: Date): string {
    var meses = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
    let diferenca = parseInt(((dataFim.getTime() - dataIni.getTime()) / 1000 / 24 / 3600).toString())
    if (diferenca < 5) {
      return `${atual.getHours()}:${atual.getMinutes()}/Dia ${atual.getDate()}`
    }
    if (diferenca < 21)
      return `${atual.getDate()}/${atual.getMonth() + 1}/${atual.getFullYear()}`
    if (diferenca < 700) {
      let semana = 0;
      if (atual.getDate() < 7) {
        semana = 1
      } else if (atual.getDate() < 14) {
        semana = 2
      } else if (atual.getDate() < 21) {
        semana = 3
      } else if (atual.getDate() < 28) {
        semana = 4
      } else {
        semana = 5
      }
      return `Sem ${semana} /${meses[atual.getMonth() + 1]}`;
    } if (diferenca < 3650) {
      return `${meses[atual.getMonth()]}/${atual.getFullYear()}`
    }
    return `${atual.getFullYear()}`
  }

  private async listaDeProdutos() {
    let produtos = await this.produtoServico.lista();
    produtos?.forEach(produto => {
      this.produtos.push(produto);
    })
    this.produtosSelecionados = this.produtos;
  }

  private async listaDePedidosProdutos() {
    let pedidosProdutos = await this.pedidoProdutoServico.lista();
    pedidosProdutos?.forEach(pedidoProduto => {
      this.pedidosProdutos.push(pedidoProduto);
    })
    this.pedidosProdutosSelecionados = this.pedidosProdutos
    this.getValor_Total();
  }

  private async listaDePedidos() {
    let pedidos = await this.pedidoServico.lista();
    pedidos?.forEach(pedido => {
      this.pedidos.push(pedido);
    })
    this.pedidosSelecionados = this.pedidos?.reverse();
  }

  private getValor_Total() {
    let total = 0, positivo = 0, negativo = 0;
    this.pedidosProdutosSelecionados.forEach(pedidoProduto => {
      let val1 = total;
      total += Number(pedidoProduto.valor) * Number(pedidoProduto.quantidade);
      if (val1 > total) {
        negativo += val1 - total
      } else {
        positivo += total - val1
      }
    })
    
    this.valorTotal = total;
    this.valorPositivo = positivo;
    this.valorNegativo = negativo;
  }

  async atualizar() {
    this.pedidosSelecionados = this.pedidos;
    this.pedidosProdutosSelecionados = this.pedidosProdutos;
    this.produtosSelecionados = this.produtos;
    let categoria = new Number(this.categoriaSelecionado.split("-")[0])
    let dictPedidoSelecionado = this.filtraData();
    let dictProdutoSelecionado = this.filtrarProduto(categoria)
    let dictPedidoTemp: Map<Number, boolean> = new Map();
    let save = this.pedidosProdutosSelecionados.filter(pedidoProduto => {
      let val = false
      if (dictPedidoSelecionado.get(pedidoProduto.pedido_id)) {
        if (dictProdutoSelecionado.get(pedidoProduto.produto_id)) {
          val = true
        }
      }
      if (!dictPedidoTemp.get(pedidoProduto.pedido_id)) dictPedidoTemp.set(pedidoProduto.pedido_id, val)
      return val;
    })
    this.pedidosProdutosSelecionados = save
    this.pedidosSelecionados = this.pedidosSelecionados.filter(pedido => {
      if (dictPedidoSelecionado.get(pedido.id)) return true
      return false;
    })
    this.getValor_Total();
    this.gerarGraficoBarra();
    this.gerarGraficoArea(categoria);
    this.gerarPie()
  }

  filtrarProduto(categoria_id: Number): Map<Number, boolean> {
    let dictProdutoSelecionado: Map<Number, boolean> = new Map();
    if (categoria_id.toString() === "0") {
      this.produtosSelecionados.forEach(produto => {
        dictProdutoSelecionado.set(produto.id, true);
      })
      return dictProdutoSelecionado
    }
    this.produtosSelecionados = this.produtosSelecionados.filter(produto => {
      if (produto.categoria_id.toString() === categoria_id.toString()) {
        dictProdutoSelecionado.set(produto.id, true)
        return true
      }
      dictProdutoSelecionado.set(produto.id, false)
      return false;
    })
    return dictProdutoSelecionado;
  }

  number(a: Number) {
    return Number(a)
  }


  filtraData(): Map<Number, boolean> {
    let dictPedidoSelecionado: Map<Number, boolean> = new Map();
    this.pedidosSelecionados = this.pedidos.filter(result => {
      let val = this.corrigirInicial(this.dataInicial) < new Date(result.data.toString()) && this.aumentar(this.dataFinal) > new Date(result.data.toString())
      dictPedidoSelecionado.set(result.id, val)
      return val
    })
    return dictPedidoSelecionado
  }
  corrigirInicial(data: String): Date {
    let val = (new Date(String(data))).getTime() + 10800000 - 1;
    return new Date(val)
  }
  aumentar(data: String): Date {
    let val = (new Date(String(data))).getTime() + 86399999 + 10800000;
    return new Date(val)
  }
}


