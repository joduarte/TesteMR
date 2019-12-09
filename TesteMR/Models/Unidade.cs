using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TesteMR.Models
{
    public class Unidade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("nId")]
        public int Id { get; set; }

        [MaxLength(10, ErrorMessage = "Este campo deve ser preenchido com até 10 caracteres")]
        [Required]
        [StringLength(10)]
        [Display(Name = "Código")]
        public string cCode { get; set; }

        [MaxLength(30, ErrorMessage = "Este campo deve ser preenchido com até 30 caracteres")]
        [StringLength(30)]
        [Display(Name = "Descrição")]
        public string cDescription { get; set; }

        [Required]
        [Display(Name = "Data de Criação")]
        public DateTime dCreateDateTime { get; set; }

        [Display(Name = "Data de Alteração")]
        public DateTime? dChangedDateTime { get; set; }

         [Required]
        public Guid uId { get; set; }


    }
}
