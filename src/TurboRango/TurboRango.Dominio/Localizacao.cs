namespace TurboRango.Dominio
{
    public class Localizacao : Entidade
    {
        public string Bairro { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Logradouro { get; set; }
    }
}
