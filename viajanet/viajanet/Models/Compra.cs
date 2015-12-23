namespace viajanet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Compra")]
    public partial class Compra
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Fk_Usuario { get; set; }

        public int FK_Viajem { get; set; }

        public int? FK_Hotel { get; set; }

        public int? Qtd_Adultos { get; set; }

        public int? Qtd_Criancas { get; set; }

        public double Valor_Total { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual Viajem Viajem { get; set; }
    }
}
