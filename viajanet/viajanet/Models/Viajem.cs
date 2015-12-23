namespace viajanet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Viajem")]
    public partial class Viajem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Viajem()
        {
            Compra = new HashSet<Compra>();
        }

        public int Id { get; set; }
       
        public int FK_Estado_Saida { get; set; }

        public int FK_Cidade_Saida { get; set; }

        public int FK_Estado_Destino { get; set; }

        public int FK_Cidade_Destino { get; set; }

        public double Valor { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Ida { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Volta { get; set; }

        public int FK_Companhia { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual Cidade Cidade1 { get; set; }

        public virtual Companhia Companhia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Estado Estado1 { get; set; }
    }
}
