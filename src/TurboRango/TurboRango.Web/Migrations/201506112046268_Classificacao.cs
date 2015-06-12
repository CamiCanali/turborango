namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Classificacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classificacaos", "MediaNota", c => c.Double(nullable: false));
            AddColumn("dbo.Classificacaos", "QtdNotas", c => c.Int(nullable: false));
            DropColumn("dbo.Classificacaos", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classificacaos", "Categoria", c => c.Int(nullable: false));
            DropColumn("dbo.Classificacaos", "QtdNotas");
            DropColumn("dbo.Classificacaos", "MediaNota");
        }
    }
}
