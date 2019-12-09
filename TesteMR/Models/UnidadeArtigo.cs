using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TesteMR.Models
{
    public class UnidadeArtigo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nId { get; set; }
                     
        [Required]
        [Column(TypeName = "decimal(20, 10)")]
        [Display(Name = "Quantidade")]
        public decimal nBaseUnitQty { get; set; }

        [Column(TypeName = "decimal(20, 8)")]
        [Display(Name = "Valor")]
        public decimal nPrice { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime dCreateDateTime { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime? dChangedDateTime { get; set; }

        [Required]
        public Guid uId { get; set; }

        [ForeignKey("Artigo")]
        [Column("uProductId")]
        public int ArtigoId { get; set; }

        
        [Display(Name = "Artigo")]
        public virtual Artigo Artigo { get; set;  }

        [ForeignKey("Unidade")]
        [Column("uUnidadeId")]
        public int UnidadeId { set; get; }

        
        [Display(Name = "Unidade")]
        public virtual Unidade Unidade { get; set; }

    }
}
