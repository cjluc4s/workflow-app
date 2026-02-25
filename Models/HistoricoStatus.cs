using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WorkflowApp.Models
{
    public class HistoricoStatus
    {
        public int Id { get; set; }

        [Required]
        public int SolicitacaoId { get; set; }

        [ForeignKey("SolicitacaoId")]
        public Solicitacao Solicitacao { get; set; } = default!;

        [Required]
        public string StatusAnterior { get; set; } = default!;

        [Required]
        public string StatusNovo { get; set; } = default!;

        [Required]
        public string UsuarioId { get; set; } = default!;

        [ForeignKey("UsuarioId")]
        public IdentityUser Usuario { get; set; } = default!;

        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}