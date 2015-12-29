namespace viajanet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        FK_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.FK_Estado)
                .Index(t => t.FK_Estado);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Telefone = c.String(nullable: false, maxLength: 50, unicode: false),
                        Longradouro = c.String(nullable: false, maxLength: 150, unicode: false),
                        FK_Estado = c.Int(nullable: false),
                        FK_Cidade = c.Int(nullable: false),
                        Bairro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Cep = c.String(nullable: false, maxLength: 50, unicode: false),
                        Img = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estado", t => t.FK_Estado)
                .ForeignKey("dbo.Cidade", t => t.FK_Cidade)
                .Index(t => t.FK_Estado)
                .Index(t => t.FK_Cidade);
            
            CreateTable(
                "dbo.Compra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_Usuario = c.String(nullable: false, maxLength: 128),
                        FK_Viajem = c.Int(nullable: false),
                        FK_Hotel = c.Int(),
                        Qtd_Adultos = c.Int(),
                        Qtd_Criancas = c.Int(),
                        Valor_Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Viajem", t => t.FK_Viajem)
                .ForeignKey("dbo.Hotel", t => t.FK_Hotel)
                .Index(t => t.FK_Viajem)
                .Index(t => t.FK_Hotel);
            
            CreateTable(
                "dbo.Viajem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FK_Estado_Saida = c.Int(nullable: false),
                        FK_Cidade_Saida = c.Int(nullable: false),
                        FK_Estado_Destino = c.Int(nullable: false),
                        FK_Cidade_Destino = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        Data_Ida = c.DateTime(nullable: false, storeType: "date"),
                        Data_Volta = c.DateTime(nullable: false, storeType: "date"),
                        FK_Companhia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companhia", t => t.FK_Companhia)
                .ForeignKey("dbo.Estado", t => t.FK_Estado_Saida)
                .ForeignKey("dbo.Estado", t => t.FK_Estado_Destino)
                .ForeignKey("dbo.Cidade", t => t.FK_Cidade_Destino)
                .ForeignKey("dbo.Cidade", t => t.FK_Cidade_Saida)
                .Index(t => t.FK_Estado_Saida)
                .Index(t => t.FK_Cidade_Saida)
                .Index(t => t.FK_Estado_Destino)
                .Index(t => t.FK_Cidade_Destino)
                .Index(t => t.FK_Companhia);
            
            CreateTable(
                "dbo.Companhia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quarto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_Hotel = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, unicode: false, storeType: "text"),
                        Diaria = c.Double(nullable: false),
                        Capacidade = c.Int(nullable: false),
                        Suite = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotel", t => t.Fk_Hotel, cascadeDelete: true)
                .Index(t => t.Fk_Hotel);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viajem", "FK_Cidade_Saida", "dbo.Cidade");
            DropForeignKey("dbo.Viajem", "FK_Cidade_Destino", "dbo.Cidade");
            DropForeignKey("dbo.Hotel", "FK_Cidade", "dbo.Cidade");
            DropForeignKey("dbo.Viajem", "FK_Estado_Destino", "dbo.Estado");
            DropForeignKey("dbo.Viajem", "FK_Estado_Saida", "dbo.Estado");
            DropForeignKey("dbo.Hotel", "FK_Estado", "dbo.Estado");
            DropForeignKey("dbo.Quarto", "Fk_Hotel", "dbo.Hotel");
            DropForeignKey("dbo.Compra", "FK_Hotel", "dbo.Hotel");
            DropForeignKey("dbo.Compra", "FK_Viajem", "dbo.Viajem");
            DropForeignKey("dbo.Viajem", "FK_Companhia", "dbo.Companhia");
            DropForeignKey("dbo.Cidade", "FK_Estado", "dbo.Estado");
            DropIndex("dbo.Quarto", new[] { "Fk_Hotel" });
            DropIndex("dbo.Viajem", new[] { "FK_Companhia" });
            DropIndex("dbo.Viajem", new[] { "FK_Cidade_Destino" });
            DropIndex("dbo.Viajem", new[] { "FK_Estado_Destino" });
            DropIndex("dbo.Viajem", new[] { "FK_Cidade_Saida" });
            DropIndex("dbo.Viajem", new[] { "FK_Estado_Saida" });
            DropIndex("dbo.Compra", new[] { "FK_Hotel" });
            DropIndex("dbo.Compra", new[] { "FK_Viajem" });
            DropIndex("dbo.Hotel", new[] { "FK_Cidade" });
            DropIndex("dbo.Hotel", new[] { "FK_Estado" });
            DropIndex("dbo.Cidade", new[] { "FK_Estado" });
            DropTable("dbo.Quarto");
            DropTable("dbo.Companhia");
            DropTable("dbo.Viajem");
            DropTable("dbo.Compra");
            DropTable("dbo.Hotel");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
        }
    }
}
