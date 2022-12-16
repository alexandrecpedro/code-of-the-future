using System;
namespace Interfaces.SOLID.ISP.Solucao
{
    public interface IGenerica
    {
        int Id { get; set; }
        void Salvar();
        void Excluir();
        void Alterar();
    }
}
