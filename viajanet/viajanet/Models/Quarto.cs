namespace viajanet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quarto")]
    public partial class Quarto
    {
        public int Id { get; set; }

        public int Fk_Hotel { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        public double Diaria { get; set; }

        public int Capacidade { get; set; }

        public bool Suite { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
