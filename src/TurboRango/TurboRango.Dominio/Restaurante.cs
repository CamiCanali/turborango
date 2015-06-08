
namespace TurboRango.Dominio
{
    public class Restaurante
    {
        public int? Capacidade { get; set; } //se não tiver valor é null
        public string Nome { get; set; }
        public Contato Contato { get; set; }
        public Localizacao Localizacao { get; set; }
        public Categoria Categoria { get; set; }


    }
}
