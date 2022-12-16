using System;
namespace Interfaces.SOLID.LSP.Solucao
{
    public abstract class Fruta
    {
        public int Id { get; set; }
        public int Nome { get; set; }
        public abstract string Cor();
    }
}
