namespace viajanet.AplicationDBContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            AddColumn("dbo.AspNetUsers", "Sobrenome", c => c.String());
            AddColumn("dbo.AspNetUsers", "Telefone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Foto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Foto");
            DropColumn("dbo.AspNetUsers", "Telefone");
            DropColumn("dbo.AspNetUsers", "Sobrenome");
            DropColumn("dbo.AspNetUsers", "Nome");
        }
    }
}
