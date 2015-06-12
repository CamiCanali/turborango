using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Classificacao : Entidade
    {
        public double Nota { get; set; }
        public double MediaNota { get; set; }
        //public Categoria Categoria { get; set; }
        //public virtual Restaurante restaurante;
        public DateTime DataAvaliacao { get; set; }
        public double calculaMedia()
        {
            return MediaNota = (MediaNota + Nota) / 2;
        }
    }
}
