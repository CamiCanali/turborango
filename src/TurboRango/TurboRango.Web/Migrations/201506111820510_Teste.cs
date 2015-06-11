namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teste : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classificacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nota = c.Double(nullable: false),
                        Categoria = c.Int(nullable: false),
                        DataAvaliacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Restaurantes", "Classificacao_Id", c => c.Int());
            CreateIndex("dbo.Restaurantes", "Classificacao_Id");
            AddForeignKey("dbo.Restaurantes", "Classificacao_Id", "dbo.Classificacaos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurantes", "Classificacao_Id", "dbo.Classificacaos");
            DropIndex("dbo.Restaurantes", new[] { "Classificacao_Id" });
            DropColumn("dbo.Restaurantes", "Classificacao_Id");
            DropTable("dbo.Classificacaos");
        }
    }
}
