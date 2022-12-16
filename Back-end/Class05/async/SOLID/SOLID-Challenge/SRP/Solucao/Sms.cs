using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.SRP.Solucao
{
    public class Sms
    {
        public bool Enviar(Cliente cliente)
        {
            //using (var sms = new SMSConnection())
            //{
            //    sms.to = "193232339223";
            //    sms.sender;
            //}

            Console.WriteLine("Salvando o objeto no banco de dados");
            return true;
        }
    }
}
