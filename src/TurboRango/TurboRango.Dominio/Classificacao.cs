using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Classificacao
    {
        public double Nota { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime DataAvaliacao { get; set; }
    }
}
