using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.OCP.SolucaoAbstracao
{
    public class Fisica : AFisicaJuridica, IPessoa
    {
        public Fisica()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public void Salvar()
        {
           Console.WriteLine("Salvando Fisica que é fornecedora");
        }
    }
}
