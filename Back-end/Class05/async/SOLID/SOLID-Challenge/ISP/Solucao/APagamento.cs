using System;
namespace Interfaces.SOLID.ISP.Solucao
{
    public abstract class APagamento : IGenerica, IPagamento
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Alterar()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            throw new NotImplementedException();
        }

        public void Salvar()
        {
            throw new NotImplementedException();
        }

        public void Pagar()
        {
            throw new NotImplementedException();
        }
    }
}
