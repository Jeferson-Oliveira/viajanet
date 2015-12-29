namespace viajanet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compra", "NumeroCartao", c => c.String());
            AddColumn("dbo.Compra", "NomeTitular", c => c.String());
            AddColumn("dbo.Compra", "Expiracao", c => c.String());
            AddColumn("dbo.Compra", "CVC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compra", "CVC");
            DropColumn("dbo.Compra", "Expiracao");
            DropColumn("dbo.Compra", "NomeTitular");
            DropColumn("dbo.Compra", "NumeroCartao");
        }
    }
}
