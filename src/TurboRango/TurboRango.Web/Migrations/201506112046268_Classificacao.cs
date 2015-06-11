namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Classificacao : DbMigration
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
            DropColumn("dbo.Restaurantes", "Classificacao_Nota");
            DropColumn("dbo.Restaurantes", "Classificacao_Categoria");
            DropColumn("dbo.Restaurantes", "Classificacao_DataAvaliacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurantes", "Classificacao_DataAvaliacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Restaurantes", "Classificacao_Categoria", c => c.Int(nullable: false));
            AddColumn("dbo.Restaurantes", "Classificacao_Nota", c => c.Double(nullable: false));
            DropForeignKey("dbo.Restaurantes", "Classificacao_Id", "dbo.Classificacaos");
            DropIndex("dbo.Restaurantes", new[] { "Classificacao_Id" });
            DropColumn("dbo.Restaurantes", "Classificacao_Id");
            DropTable("dbo.Classificacaos");
        }
    }
}
