using System;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.DIP.QuebrandoRegra
{
    public class Cliente
    {
        public Cliente()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public bool Valido()
        {
            return string.IsNullOrEmpty(this.Nome) ? false : true;
        }
    }
}
