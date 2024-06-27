using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicoAPI.Models
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int ServicoId { get; set; }
        [Required]
        public decimal PrecoCobrado { get; set; }
        [Required]
        public DateTime DataContratacao { get; set; }

        [ForeignKey("ServicoId")]
        public Servico Servico { get; set; }  
    }
}
