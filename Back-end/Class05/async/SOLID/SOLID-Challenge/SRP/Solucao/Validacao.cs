using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.SRP.Solucao
{
    public class Validacao
    {
        public void Validar(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nome))
            {
                throw new Exception("O Nome é obrigatório");
            }

            if (string.IsNullOrEmpty(cliente.Telefone))
            {
                throw new Exception("O Telefone é obrigatório");
            }
        }
    }
}
