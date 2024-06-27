using System.ComponentModel.DataAnnotations;

namespace ServicoAPI.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}

