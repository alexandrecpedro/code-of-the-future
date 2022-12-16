using System;
namespace Interfaces.SOLID.ISP.QuebrandoRegra
{
    public interface IGenerica
    {
        int Id { get; set; }
        void Salvar();
        void Excluir();
        void Alterar();
        void Pagar();
    }
}
