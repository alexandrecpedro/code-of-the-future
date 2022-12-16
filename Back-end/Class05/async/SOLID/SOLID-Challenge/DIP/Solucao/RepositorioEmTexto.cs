using System;
using System.Collections.Generic;
using System.Data;

namespace Interfaces.SOLID.DIP.Solucao
{
    public class RepositorioEmTexto : IRepositorio
    {
        public bool Salvar(ref Cliente cliente)
        {
            Console.WriteLine("Salvando o objeto no banco de dados em texto");
            return true;
        }
    }
}
