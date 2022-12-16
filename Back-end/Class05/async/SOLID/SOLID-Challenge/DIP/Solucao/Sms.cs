using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.DIP.Solucao
{
    public class Sms
    {
        public bool Enviar(Cliente cliente, ISms iSms)
        {
            //using (var sms = new SMSConnection())
            //{
            //    sms.to = "193232339223";
            //    sms.sender;
            //}
            iSms.Enviar();
            Console.WriteLine("Salvando o objeto no banco de dados");
            return true;
        }
    }
}
