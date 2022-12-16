using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.DIP.Solucao
{
    public interface ISms
    {
        string De { get; set; }
        string Para { get; set; }
        void Enviar();
    }
}
