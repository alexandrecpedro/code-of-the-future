using System;
namespace Interfaces.SOLID.ISP.Solucao
{
    public class Produto : IGenerica
    {
        public Produto()
        {
        }

        public int Id { get; set; }

        public void Alterar()
        {
            Console.WriteLine("11111");
        }

        public void Excluir()
        {
            Console.WriteLine("22222");
        }

        public void Salvar()
        {
            Console.WriteLine("33333");
        }
    }
}
