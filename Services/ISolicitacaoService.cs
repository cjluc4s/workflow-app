using WorkflowApp.Models;

namespace WorkflowApp.Services
{
    public interface ISolicitacaoService
    {
        Task CriarSolicitacaoAsync(Solicitacao solicitacao, string usuarioId);

        Task AprovarSolicitacaoAsync(int id, string usuarioId);

        Task RejeitarSolicitacaoAsync(int id, string usuarioId);
    }
}