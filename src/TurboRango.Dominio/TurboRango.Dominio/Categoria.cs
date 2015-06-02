using System.ComponentModel;

namespace TurboRango.Dominio{
    internal enum Categoria
    {
        [Description("Comum")]
        Comum,
        [Description("Cozinha Natural")]
        CozinhaNatural,
        [Description("Cozinha Mexicana")]
        CozinhaMexicana,
        [Description("Churrascaria")]
        Churrascaria,
        [Description("Cozinha Japonesa")]
        CozinhaJaponesa,
        [Description("Fast food")]
        Fastfood,
        [Description("Pizzaria")]
        Pizzaria
        
    }
}
