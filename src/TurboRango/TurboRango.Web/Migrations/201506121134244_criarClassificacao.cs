namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criarClassificacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classificacoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nota = c.Double(nullable: true),
                    MediaNota = c.Double(nullable: true),
                    DataAvaliacao = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
        }
    }
}
