namespace viajanet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserRoles : DbContext
    {
        public UserRoles()
            : base("name=ViajanetContext")
        {
        }

        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
