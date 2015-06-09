using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class CarinhaQueManipulaOBanco
    {
        // readonly? https://msdn.microsoft.com/pt-br/library/acdd6hb7.aspx
        readonly string connectionString;
        // convenção para constantes => TUDO_MAIUSCULO_SEPARADO_POR_UNDERLINE
        // readonly static = para ser criado apenas uma vez (por classe, não por instância)
        readonly static string INSERT_SQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
        readonly static string SELECT_SQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato] (nolock)";

        public CarinhaQueManipulaOBanco(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void Inserir(Contato contato)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var inserirContato = new SqlCommand(INSERT_SQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;

                    connection.Open();
                    int idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());

                    // Debug? Olhe a aba "Output" no rodapé do Visual Studio e escolha "Debug" em "Show output from"
                    Debug.WriteLine("Contato criado! ID no banco: {0}", idCriado);
                }
            }
        }

        internal IEnumerable<Contato> GetContatos()
        {
            var contatos = new List<Contato>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var lerContatos = new SqlCommand(SELECT_SQL, connection))
                {
                    connection.Open();

                    var reader = lerContatos.ExecuteReader();

                    while (reader.Read())
                    {
                        contatos.Add(new Contato
                        {
                             Site = reader.IsDBNull(0) ? null : reader.GetString(0),
                             Telefone = reader.IsDBNull(1) ? null : reader.GetString(1)
                        });
                    }
                }
            }

            return contatos;
        }
    }
}
