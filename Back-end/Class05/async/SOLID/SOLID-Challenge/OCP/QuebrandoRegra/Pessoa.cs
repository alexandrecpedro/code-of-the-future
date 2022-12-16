using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.OCP.QuebrandoRegra
{
    public class Pessoa
    {
        public Pessoa()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoPessoa Tipo { get; set; }

        public void Salvar()
        {
            if(this.Tipo == TipoPessoa.Fisica)
            {
                Console.WriteLine("Salvando fisica");
            }
            else if(this.Tipo == TipoPessoa.Juridica)
            {
                Console.WriteLine("Salvando juridica");
            }
            else if (this.Tipo == TipoPessoa.Fornecedor)
            {
                Console.WriteLine("Salvando fornecedor");
            }
        }
    }
}
