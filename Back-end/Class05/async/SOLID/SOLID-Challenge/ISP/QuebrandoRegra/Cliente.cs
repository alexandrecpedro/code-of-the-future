using System;
namespace Interfaces.SOLID.ISP.QuebrandoRegra
{
    public class Cliente : IGenerica
    {
        public Cliente()
        {
        }

        public int Id { get; set; }

        public void Alterar()
        {
            Console.WriteLine("111111");
        }

        public void Excluir()
        {
            Console.WriteLine("22222");
        }

        public void Pagar()
        {
            throw new NotImplementedException();
        }

        public void Salvar()
        {
            Console.WriteLine("33333");
        }
    }
}
