using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.SRP.QuebrandoRegra
{
    public class Cliente
    {
        public Cliente() { }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public void Salvar()
        {
            if (!this.Validado()) throw new Exception("Erro na validação de dados");

            Console.WriteLine("Salvando o objeto no banco de dados");
            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();
                cn.ConnectionString = "MinhaStrConnection";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into clientes ( ...";

                cn.Open();
                this.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            Console.WriteLine("Salvando o objeto no banco de dados");

            this.EnviarSMS();
        }

        private bool Validado()
        {
            Console.WriteLine("Validando classe");
            return true;
        }

        public void Excluir()
        {
            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();
                cn.ConnectionString = "MinhaStrConnection";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from clientes where id=" + this.Id;

                cn.Open();
                cmd.ExecuteNonQuery();

                Console.WriteLine("Salvando o objeto no banco de dados");
            }
        }

        public bool EnviarSMS()
        {
            //using (var sms = new SMSConnection())
            //{
            //    sms.to = "193232339223";
            //    sms.sender;
            //}

            Console.WriteLine("Salvando o objeto no banco de dados");
            return true;
        }

        public static List<Cliente> Buscar()
        {
            Console.WriteLine("Buscando lista de clientes");
            return new List<Cliente>();
        }
    }
}
