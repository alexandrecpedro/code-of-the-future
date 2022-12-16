using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.DIP.Solucao
{
    public class SmsOi : ISms
    {
        public string De { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Para { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Enviar()
        {
            Console.WriteLine("Salvando o objeto no banco de dados");
        }
    }
}
