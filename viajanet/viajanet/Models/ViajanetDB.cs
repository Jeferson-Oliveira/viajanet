namespace viajanet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class viajanetDB : DbContext
    {
        public viajanetDB()
            : base("name=viajanetDB")
        {
        }

        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Companhia> Companhia { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Quarto> Quarto { get; set; }
        public virtual DbSet<Viajem> Viajem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.Hotel)
                .WithRequired(e => e.Cidade)
                .HasForeignKey(e => e.FK_Cidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.Viajem)
                .WithRequired(e => e.Cidade)
                .HasForeignKey(e => e.FK_Cidade_Destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.Viajem1)
                .WithRequired(e => e.Cidade1)
                .HasForeignKey(e => e.FK_Cidade_Saida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Companhia>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Companhia>()
                .HasMany(e => e.Viajem)
                .WithRequired(e => e.Companhia)
                .HasForeignKey(e => e.FK_Companhia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.Sigla)
                .IsFixedLength();

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidade)
                .WithRequired(e => e.Estado)
                .HasForeignKey(e => e.FK_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Hotel)
                .WithRequired(e => e.Estado)
                .HasForeignKey(e => e.FK_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Viajem)
                .WithRequired(e => e.Estado)
                .HasForeignKey(e => e.FK_Estado_Saida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Viajem1)
                .WithRequired(e => e.Estado1)
                .HasForeignKey(e => e.FK_Estado_Destino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Telefone)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Longradouro)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Cep)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Img)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Compra)
                .WithOptional(e => e.Hotel)
                .HasForeignKey(e => e.FK_Hotel);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Quarto)
                .WithRequired(e => e.Hotel)
                .HasForeignKey(e => e.Fk_Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quarto>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Viajem>()
                .HasMany(e => e.Compra)
                .WithRequired(e => e.Viajem)
                .HasForeignKey(e => e.FK_Viajem)
                .WillCascadeOnDelete(false);
        }
    }
}
