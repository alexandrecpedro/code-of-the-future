using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.OCP.SolucaoAbstracao
{
    public abstract class AFisicaJuridica : AFisica
    {
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
    }
}
