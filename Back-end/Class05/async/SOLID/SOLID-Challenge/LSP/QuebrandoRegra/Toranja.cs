using System;
namespace Interfaces.SOLID.LSP.QuebrandoRegra
{
    public class Toranja
    {
        public int Id { get; set; }
        public int Nome { get; set; }

        public virtual string Cor()
        {
            return "Vermelha";
        }
    }
}
