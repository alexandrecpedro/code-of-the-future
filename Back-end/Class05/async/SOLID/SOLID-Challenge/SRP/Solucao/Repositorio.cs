﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.SRP.Solucao
{
    public class Repositorio
    {
        public void Salvar(ref Cliente cliente)
        {
            Console.WriteLine("Salvando o objeto no banco de dados");
            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();
                cn.ConnectionString = "MinhaStrConnection";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into clientes ( ...";

                cn.Open();
                cliente.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            Console.WriteLine("Salvando o objeto no banco de dados");
        }
    }
}
