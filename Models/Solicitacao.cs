using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WorkflowApp.Models
{
    public class Solicitacao
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = default!;

        [Required]
        public string Descricao { get; set; } = default!;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pendente";

        public string UsuarioId { get; set; } = default!;

        [ForeignKey("UsuarioId")]
        public IdentityUser? Usuario { get; set; }
    }
}