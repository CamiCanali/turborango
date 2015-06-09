namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TurboRango.Dominio;

    internal sealed class Configuration : DbMigrationsConfiguration<TurboRango.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TurboRango.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(TurboRango.Web.Models.ApplicationDbContext context)
        {
            context.Restaurantes.AddOrUpdate(
                r => r.Nome,
                new Restaurante
                {
                    Nome = "GARFÃO RESTAURANTE E PIZZARIA",
                    Capacidade = 100,
                    Categoria = Categoria.Comum,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua Sete de Setembro, 1045 - Liberdade",
                        Bairro = "Liberdade",
                        Latitude = -29.712571,
                        Longitude = -51.13636
                    },
                    Contato = new Contato
                    {
                        Site = "www.garfao.com",
                        Telefone = "(51) 3587-7700"
                    }
                },
                new Restaurante
                {
                    Nome = "SEMENTE",
                    Capacidade = 40,
                    Categoria = Categoria.CozinhaNatural,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua: Joaquim Pedro Soares, 633",
                        Bairro = "Centro",
                        Latitude = -29.6812707,
                        Longitude = -51.1272292
                    },
                    Contato = new Contato
                    {
                        Telefone = "3595-5258"
                    }
                },
                new Restaurante
                {
                    Nome = "OLÉ ARMAZÉM MEXICANO",
                    Capacidade = 30,
                    Categoria = Categoria.CozinhaMexicana,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua Gomes Portinho, 448",
                        Bairro = "Centro",
                        Latitude = -29.682078,
                        Longitude = -51.125199
                    },
                    Contato = new Contato
                    {
                        Telefone = "3279-8828"
                    }
                },
                new Restaurante
                {
                    Nome = "CHURRASCARIA PRIMAVERA",
                    Capacidade = 110,
                    Categoria = Categoria.Churrascaria,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "BR 116, 2554, Km 31",
                        Bairro = "Primavera",
                        Latitude = -29.693741,
                        Longitude = -51.144554
                    },
                    Contato = new Contato
                    {
                        Site = "www.grupoprimaveranh.com.br",
                        Telefone = "3595-8081"
                    }
                },
                new Restaurante
                {
                    Nome = "SUSHI DAI",
                    Capacidade = 40,
                    Categoria = Categoria.CozinhaJaponesa,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Bagé, 149",
                        Bairro = "Boa Vista",
                        Latitude = -29.68942,
                        Longitude = -51.125224
                    }
                },
                new Restaurante
                {
                    Nome = "TAKESHI",
                    Capacidade = 35,
                    Categoria = Categoria.CozinhaJaponesa,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua Confraternização, 792",
                        Bairro = "Pátria Nova",
                        Latitude = -29.698669,
                        Longitude = -51.130195
                    },
                    Contato = new Contato
                    {
                        Telefone = "3066-6660"
                    }
                },
                new Restaurante
                {
                    Nome = "HAI SAIKÔ",
                    Capacidade = 75,
                    Categoria = Categoria.CozinhaJaponesa,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua Gomes Portinho, 730",
                        Bairro = "Mauá",
                        Latitude = -29.684873,
                        Longitude = -51.122252
                    },
                    Contato = new Contato
                    {
                        Site = "www.haisaiko.com.br",
                        Telefone = "3593-5757"
                    }
                },
                new Restaurante
                {
                    Nome = "PICA-PAU LANCHES",
                    Capacidade = 30,
                    Categoria = Categoria.Fastfood,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua: José do Patrocínio, 880",
                        Bairro = "Rio Branco",
                        Latitude = -29.68339,
                        Longitude = -51.135376
                    },
                    Contato = new Contato
                    {
                        Site = "www.picapaulanches.com",
                        Telefone = "(51) 3593-8079"
                    }
                },
                new Restaurante
                {
                    Nome = "FOFINHO LANCHES",
                    Capacidade = 80,
                    Categoria = Categoria.Fastfood,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua General Osório, 312",
                        Bairro = "Hamburgo Velho",
                        Latitude = -29.688934,
                        Longitude = -51.137052
                    },
                    Contato = new Contato
                    {
                        Telefone = "35942512"
                    }
                },
                new Restaurante
                {
                    Nome = "RODA PIZZA GIRASSOL",
                    Capacidade = 150,
                    Categoria = Categoria.Pizzaria,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Joaquim Nabuco, 1099 Loja. 01",
                        Bairro = "Centro",
                        Latitude = -29.6829373,
                        Longitude = -51.1264753
                    },
                    Contato = new Contato
                    {
                        Telefone = "3595-5766"
                    }
                },
                new Restaurante
                {
                    Nome = "LA CARBONERA",
                    Capacidade = 135,
                    Categoria = Categoria.Pizzaria,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Avenida Dr. Maurício Cardoso, 1904",
                        Bairro = "Hamburgo Velho",
                        Latitude = -29.6871764,
                        Longitude = -51.1217197
                    },
                    Contato = new Contato
                    {
                        Telefone = "35252020"
                    }
                },
                new Restaurante
                {
                    Nome = "PIZZARIA MASTROIANI",
                    Capacidade = 140,
                    Categoria = Categoria.Pizzaria,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Avenida Pedro Adams Filho, 4668",
                        Bairro = "Centro",
                        Latitude = -29.6915801,
                        Longitude = -51.1286563
                    },
                    Contato = new Contato
                    {
                        Telefone = "35948400"
                    }
                },
                new Restaurante
                {
                    Nome = "RESTAURANTE FIRENZE",
                    Capacidade = 200,
                    Categoria = Categoria.Comum,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Avenida Pedro Adams Filho, 4668",
                        Bairro = "Centro",
                        Latitude = -29.6828043,
                        Longitude = -51.1287372
                    },
                    Contato = new Contato
                    {
                        Telefone = "35822841"
                    }
                },
                new Restaurante
                {
                    Nome = "RESTAURANTE LOCATELLI",
                    Capacidade = 400,
                    Categoria = Categoria.Comum,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Rua Borges do Canto, 121",
                        Bairro = "Hamburgo Velho",
                        Latitude = -29.6756157,
                        Longitude = -51.1156472
                    },
                    Contato = new Contato
                    {
                        Telefone = "35276229"
                    }
                },
                new Restaurante
                {
                    Nome = "DEL PIETRO",
                    Capacidade = 115,
                    Categoria = Categoria.Comum,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Avenida Dr. Maurício Cardoso, 137",
                        Bairro = "Hamburgo Velho",
                        Latitude = -29.6747349,
                        Longitude = -51.1116387
                    },
                    Contato = new Contato
                    {
                        Telefone = "35813333"
                    }
                },
                new Restaurante
                {
                    Nome = "ADAMS PIZZARIA",
                    Capacidade = 130,
                    Categoria = Categoria.Pizzaria,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Avenida Pedro Adams Filho, 4466",
                        Bairro = "Industrial",
                        Latitude = -29.6933741,
                        Longitude = -51.1283531
                    },
                    Contato = new Contato
                    {
                        Telefone = "35823000"
                    }
                },
                new Restaurante
                {
                    Nome = "RESTAURANTE HABITUÉ",
                    Capacidade = 90,
                    Categoria = Categoria.Comum,
                    Localizacao = new Localizacao
                    {
                        Logradouro = "Joaquim Nabuco, 1117",
                        Bairro = "Centro",
                        Latitude = -29.6829086,
                        Longitude = -51.1263546
                    },
                    Contato = new Contato
                    {
                        Telefone = "35931602"
                    }
                }
                );
        }
    }
}
