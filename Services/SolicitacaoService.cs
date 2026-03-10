using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkflowApp.Data;
using WorkflowApp.Models;

namespace WorkflowApp.Services
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly ApplicationDbContext _context;

        public SolicitacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CriarSolicitacaoAsync(Solicitacao solicitacao, string usuarioId)
        {
            solicitacao.UsuarioId = usuarioId;
            solicitacao.DataCriacao = DateTime.Now;
            solicitacao.Status = StatusSolicitacao.Pendente;

            _context.Solicitacoes.Add(solicitacao);

            await _context.SaveChangesAsync();
        }

        public async Task AprovarSolicitacaoAsync(int id, string usuarioId)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);

            if (solicitacao == null)
                throw new Exception("Solicitação não encontrada.");

            var statusAnterior = solicitacao.Status;

            solicitacao.Status = StatusSolicitacao.Aprovado;

            var historico = new HistoricoStatus
            {
                SolicitacaoId = solicitacao.Id,
                StatusAnterior = statusAnterior.ToString(),
                StatusNovo = StatusSolicitacao.Aprovado.ToString(),
                UsuarioId = usuarioId,
                DataAlteracao = DateTime.Now
            };

            _context.HistoricosStatus.Add(historico);

            await _context.SaveChangesAsync();
        }

        public async Task RejeitarSolicitacaoAsync(int id, string usuarioId)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);

            if (solicitacao == null)
                throw new Exception("Solicitação não encontrada.");

            var statusAnterior = solicitacao.Status;

            solicitacao.Status = StatusSolicitacao.Rejeitado;

            var historico = new HistoricoStatus
            {
                SolicitacaoId = solicitacao.Id,
                StatusAnterior = statusAnterior.ToString(),
                StatusNovo = StatusSolicitacao.Rejeitado.ToString(),
                UsuarioId = usuarioId,
                DataAlteracao = DateTime.Now
            };

            _context.HistoricosStatus.Add(historico);

            await _context.SaveChangesAsync();
        }
    }
}