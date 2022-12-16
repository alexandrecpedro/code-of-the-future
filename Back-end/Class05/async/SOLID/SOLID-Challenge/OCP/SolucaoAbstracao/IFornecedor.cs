using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.OCP.SolucaoAbstracao
{
    public interface IFornecedor
    {
        string Credenciamento { get; set; }
    }
}
