using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.OCP.SolucaoAbstracao
{
    public interface IJuridica
    {
        string CNPJ { get; set; }
        string InscricaoEstadual { get; set; }
    }
}
