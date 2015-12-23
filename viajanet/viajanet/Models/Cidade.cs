namespace viajanet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cidade")]
    public partial class Cidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cidade()
        {
            Hotel = new HashSet<Hotel>();
            Viajem = new HashSet<Viajem>();
            Viajem1 = new HashSet<Viajem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public int FK_Estado { get; set; }

        public virtual Estado Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hotel> Hotel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viajem> Viajem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viajem> Viajem1 { get; set; }
    }
}
