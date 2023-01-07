```shell


dotnet aspnet-codegenerator controller -name ClientesController -m Cliente -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout
dotnet aspnet-codegenerator controller -name MarcasController -m Marca -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout
dotnet aspnet-codegenerator controller -name ModelosController -m Modelo -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout
dotnet aspnet-codegenerator controller -name CarrosController -m Carro -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout
dotnet aspnet-codegenerator controller -name PedidosController -m Pedido -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout
dotnet aspnet-codegenerator controller -name ConfiguracoesController -m Configuracao -dc LocacaoContext --relativeFolderPath Controllers --useDefaultLayout

```