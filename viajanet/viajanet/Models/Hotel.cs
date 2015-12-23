namespace viajanet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hotel")]
    public partial class Hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotel()
        {
            Compra = new HashSet<Compra>();
            Quarto = new HashSet<Quarto>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(150)]
        public string Longradouro { get; set; }

        public int FK_Estado { get; set; }

        public int FK_Cidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(50)]
        public string Cep { get; set; }

        [StringLength(250)]
        public string Img { get; set; }

        public virtual Cidade Cidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }

        public virtual Estado Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quarto> Quarto { get; set; }
    }
}
