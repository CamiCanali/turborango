using Importador.XML;
using System;
using System.Collections.Generic;
using System.Text;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Linq to XML
            const string nomeArquivo = "restaurantes.xml";

            var restaurantesXML = new RestaurantesXML(nomeArquivo);
            var nomes = restaurantesXML.ObterSites();
            var capacidadeMedia = restaurantesXML.CapacidadeMedia();
            var capacidadeMaxima = restaurantesXML.CapacidadeMaxima();

            var ex1a = restaurantesXML.OrdernarPorNomeAsc();
            var ex1d = restaurantesXML.AgruparPorCategoria();
            var ex1e = restaurantesXML.ApenasComUmRestaurante();
            var ex1f = restaurantesXML.ApenasMaisPopulares();
            var ex1g = restaurantesXML.BairrosComMenosPizzarias();
            var ex1h = restaurantesXML.AgrupadosPorBairroPercentual();

            var todos = restaurantesXML.TodosRestaurantes();
            #endregion

            #region ADO.NET
            var connString = @"Data Source=.;Initial Catalogo=TurboRango_dev;Integrated Security=True;";
            //UID=sa;PWD=feevale no local de integrated security
            var acessoAoBanco = new Importador.XML.CarinhaQueManipulaOBanco(connString);
            acessoAoBanco.Inserir(new Contato
            {
                Site = "www.dogao.gif",
                Telefone = "555555" 
            });

            IEnumerable<Contato> contatos = acessoAoBanco.GetContato();
            #endregion

            #region Exercicios

            Restaurante restaurante = new Restaurante();

            //restaurante.
            //Console.Write(restaurante.Contato.Site);
            Console.WriteLine(
                restaurante.Capacidade.HasValue ?
                    restaurante.Capacidade.Value.ToString() :
                    "oi"
                );

            restaurante.Nome = string.Empty + " ";

            Console.WriteLine(restaurante.Nome ?? "Nulo!!!");

            Console.WriteLine(!string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "tem valor" : "não tem valor");

            var oQueEuGosto = "bacon";

            var texto = String.Format("Eu gosto de {0}", oQueEuGosto);
            // var texto = String.Format("Eu gosto de \{oQueEuGosto}");

            StringBuilder pedreiro = new StringBuilder();
            pedreiro.AppendFormat("Eu gosto de {0}", oQueEuGosto);
            pedreiro.Append("!!!!!!");

            object obj = "1";
            int a = Convert.ToInt32(obj);
            int convertido = 10;
            bool conseguiu = Int32.TryParse("1gdfgfd", out convertido);
            int res = 12 + a;

            Console.WriteLine(pedreiro);

            #endregion
        }
    }
}
