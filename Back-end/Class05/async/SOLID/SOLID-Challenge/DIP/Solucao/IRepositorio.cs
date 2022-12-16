using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Interfaces.SOLID.DIP.Solucao
{
    public interface IRepositorio
    {
        bool Salvar(ref Cliente cliente);
    }
}
