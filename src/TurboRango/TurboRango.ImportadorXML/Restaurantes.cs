using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TurboRango.Dominio;
using TurboRango.Dominio.Utils;

namespace TurboRango.ImportadorXML
{
    public class Restaurantes
    {
        readonly static string INSERT_SQL = "INSERT INTO [dbo].[Restaurante] ([Capacidade],[Nome],[Categoria],[ContatoId],[LocalizacaoId]) VALUES (@Capacidade, @Nome, @Categoria, @ContatoId, @LocalizacaoId);";
        readonly static string DELETE_SQL = "DELETE [dbo].[Restaurante] WHERE [Id] = @Id";
        readonly static string SELECT_FKS = "SELECT [ContatoId], [LocalizacaoId] FROM [dbo].[Restaurante] WITH (nolock) WHERE [Id] = @Id";
        readonly static string SELECT_JOINS = "SELECT r.*, c.*, l.* FROM [dbo].[Restaurante] r INNER JOIN dbo.[Contato] c on r.ContatoId = c.Id INNER JOIN dbo.[Localizacao] l on r.LocalizacaoId = l.Id";
        readonly static string UPDATE_SQL = "UPDATE [dbo].[Restaurante] SET [Capacidade] = @Capacidade, [Nome] = @Nome, [Categoria] = @Categoria WHERE [Id] = @Id";

        string ConnectionString { get; set; }
        Contatos ListaContatos { get; set; }
        Localizacoes ListaLocalizacoes { get; set; }

        public Restaurantes(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.ListaContatos = new Contatos(connectionString);
            this.ListaLocalizacoes = new Localizacoes(connectionString);
        }

        internal void Inserir(Restaurante restaurante)
        {
            int novoContato = ListaContatos.Inserir(restaurante.Contato);
            int novaLocalizacao = ListaLocalizacoes.Inserir(restaurante.Localizacao);
            ExecutarSQLInsert(restaurante, novoContato, novaLocalizacao);
        }

        internal void Remover(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                DataTable tabela = new DataTable();

                int contatoId, localizacaoId;

                using (var selectFks = new SqlCommand(SELECT_FKS, connection))
                {
                    selectFks.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    tabela.Load(selectFks.ExecuteReader());

                    contatoId = Convert.ToInt32(tabela.Rows[0]["ContatoId"]);
                    localizacaoId = Convert.ToInt32(tabela.Rows[0]["LocalizacaoId"]);
                }

                using (var transaction = connection.BeginTransaction())
                {
                    using (var removerRestaurante = new SqlCommand(DELETE_SQL, connection))
                    {
                        removerRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        removerRestaurante.ExecuteNonQuery();
                    }

                    ListaContatos.Remover(contatoId);
                    ListaLocalizacoes.Remover(localizacaoId);
                }
            }
        }

        internal void Atualizar(int id, Restaurante restaurante)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                DataTable tabela = new DataTable();

                int contatoId, localizacaoId;

                using (var selectFks = new SqlCommand(SELECT_FKS, connection))
                {
                    selectFks.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    tabela.Load(selectFks.ExecuteReader());

                    contatoId = Convert.ToInt32(tabela.Rows[0]["ContatoId"]);
                    localizacaoId = Convert.ToInt32(tabela.Rows[0]["LocalizacaoId"]);
                }

                ListaContatos.Atualizar(contatoId, restaurante.Contato);
                ListaLocalizacoes.Atualizar(localizacaoId, restaurante.Localizacao);

                using (var atualizarRestaurante = new SqlCommand(UPDATE_SQL, connection))
                {
                    atualizarRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    atualizarRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    atualizarRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria.ToString();
                    atualizarRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    atualizarRestaurante.ExecuteNonQuery();
                }
            }
        }

        internal IEnumerable<Restaurante> Todos()
        {
            DataTable tabela = new DataTable();

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var listarRestaurantes = new SqlCommand(SELECT_JOINS, connection))
                {
                    connection.Open();
                    tabela.Load(listarRestaurantes.ExecuteReader());

                    foreach (DataRow linha in tabela.Rows)
                    {
                        yield return new Restaurante
                        {
                            Capacidade = Convert.ToInt32(linha["Capacidade"]),
                            Nome = Convert.ToString(linha["Nome"]),
                            Categoria = Convert.ToString(linha["Categoria"]).ToEnum<Categoria>(),
                            Contato = new Contato
                            {
                                Site = Convert.ToString(linha["Site"]),
                                Telefone = Convert.ToString(linha["Telefone"])
                            },
                            Localizacao = new Localizacao
                            {
                                Bairro = Convert.ToString(linha["Bairro"]),
                                Logradouro = Convert.ToString(linha["Logradouro"]),
                                Latitude = Convert.ToDouble(linha["Latitude"]),
                                Longitude = Convert.ToDouble(linha["Longitude"])
                            }
                        };
                    }
                }
            }
        }

        void ExecutarSQLInsert(Restaurante restaurante, int fkNovoContato, int fkNovaLocalizacao)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var inserirRestaurante = new SqlCommand(INSERT_SQL, connection))
                {
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria.ToString();
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = fkNovoContato;
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = fkNovaLocalizacao;
                    connection.Open();
                    inserirRestaurante.ExecuteNonQuery();
                }
            }
        }

        // Abaixo seguem dois exemplos de "Classes aninhadas".
        // Elas foram criadas como aninhadas pois não queremos acessá-las de fora da classe Restaurante, mas queremos utilizá-las como objetos dentro da classe Restaurante.
        // Documentação: https://msdn.microsoft.com/pt-br/library/ms173120.aspx
        class Contatos
        {
            readonly static string INSERT_SQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
            readonly static string DELETE_SQL = "DELETE [dbo].[Contato] WHERE [Id] = @Id";
            readonly static string UPDATE_SQL = "UPDATE [dbo].[Contato] SET [Site] = @Site, [Telefone] = @Telefone WHERE [Id] = @Id";

            string ConnectionString { get; set; }

            internal Contatos(string connString)
            {
                ConnectionString = connString;
            }

            internal int Inserir(Contato contato)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var inserirContato = new SqlCommand(INSERT_SQL, connection))
                    {
                        inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object)DBNull.Value;
                        inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object)DBNull.Value;
                        connection.Open();
                        return Convert.ToInt32(inserirContato.ExecuteScalar());
                    }
                }
            }

            internal void Remover(int id)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var removerContato = new SqlCommand(DELETE_SQL, connection))
                    {
                        removerContato.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                        connection.Open();
                        removerContato.ExecuteNonQuery();
                    }
                }
            }

            internal void Atualizar(int id, Contato contato)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var atualizarContato = new SqlCommand(UPDATE_SQL, connection))
                    {
                        atualizarContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                        atualizarContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;
                        atualizarContato.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        atualizarContato.ExecuteNonQuery();
                    }
                }
            }
        }

        class Localizacoes
        {
            readonly static string INSERT_SQL = "INSERT INTO [dbo].[Localizacao] ([Bairro],[Logradouro],[Latitude],[Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude); SELECT @@IDENTITY";
            readonly static string DELETE_SQL = "DELETE [dbo].[Localizacao] WHERE [Id] = @Id";
            readonly static string UPDATE_SQL = "UPDATE [dbo].[Localizacao] SET [Bairro] = @Bairro, [Logradouro] = @Logradouro, [Latitude] = @Latitude, [Longitude] = @Longitude WHERE [Id] = @Id";
            string ConnectionString { get; set; }

            internal Localizacoes(string connString)
            {
                ConnectionString = connString;
            }

            internal int Inserir(Localizacao localizacao)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var inserirLocalizacao = new SqlCommand(INSERT_SQL, connection))
                    {
                        inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                        inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                        inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                        inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;
                        connection.Open();
                        return Convert.ToInt32(inserirLocalizacao.ExecuteScalar());
                    }
                }
            }

            internal void Remover(int id)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var removerLocalizacoes = new SqlCommand(DELETE_SQL, connection))
                    {
                        removerLocalizacoes.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                        connection.Open();
                        removerLocalizacoes.ExecuteNonQuery();
                    }
                }
            }

            internal void Atualizar(int id, Localizacao localizacao)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var atualizarContato = new SqlCommand(UPDATE_SQL, connection))
                    {
                        atualizarContato.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                        atualizarContato.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                        atualizarContato.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                        atualizarContato.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;
                        atualizarContato.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        connection.Open();
                        atualizarContato.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
