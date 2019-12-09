using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TesteMR.Models
{
    public class Artigo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("nId")]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "Este campo deve ser preenchido com até 30 caracteres")]
        [StringLength(30)]
        [Required]
        [Display(Name = "Código")]
        public string cCode { get; set; }

        [MaxLength(150, ErrorMessage = "Este campo deve ser preenchido com até 150 caracteres")]
        [StringLength(150)]
        [Display(Name = "Descrição")]
        public string cDescription { get; set; }

        [MaxLength(10, ErrorMessage = "Este campo deve ser preenchido com até 10 caracteres")]
        [StringLength(10)]
        [Required]
        [Display(Name = "Unidade Medida Base")]
        public string cUnitCode { get; set; }

        [Column(TypeName = "decimal(20, 8)")]
        [Display(Name = "P.V.P")]
        public decimal nUnitPrice { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime dCreateDateTime { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime? dChangedDateTime { get; set; }

        public Guid uId { get; set; }

        public virtual ICollection<UnidadeArtigo> UnidadesArtigos { get; set; }

    }

    public enum UnitCode
    {
        Kgs = 1,
        Metros = 2
    }
}
