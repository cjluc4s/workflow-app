using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApp.Data;
using WorkflowApp.Models;

namespace WorkflowApp.Controllers
{
    [Authorize]
    public class SolicitacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SolicitacoesController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==========================================
        // LISTAR - Apenas do usuário logado
        // ==========================================
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var solicitacoes = await _context.Solicitacoes
                .Where(s => s.UsuarioId == userId)
                .ToListAsync();

            return View(solicitacoes);
        }

        // ==========================================
        // PAINEL DE APROVAÇÕES - Apenas Aprovador
        // ==========================================
        [Authorize(Roles = "Aprovador")]
        public async Task<IActionResult> Aprovacoes()
        {
            var pendentes = await _context.Solicitacoes
                .Include(s => s.Usuario)
                .Where(s => s.Status == "Pendente")
                .ToListAsync();

            return View(pendentes);
        }

        // ==========================================
        // CREATE (GET)
        // ==========================================
        public IActionResult Create()
        {
            return View();
        }

        // ==========================================
        // CREATE (POST)
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Solicitacao solicitacao)
        {
            ModelState.Remove("UsuarioId");
            ModelState.Remove("Usuario");

            if (!ModelState.IsValid)
                return View(solicitacao);

            solicitacao.UsuarioId = _userManager.GetUserId(User)!;
            solicitacao.DataCriacao = DateTime.Now;
            solicitacao.Status = "Pendente";

            _context.Add(solicitacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // APROVAR
        // ==========================================
        [Authorize(Roles = "Aprovador")]
        public async Task<IActionResult> Aprovar(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);

            if (solicitacao == null)
                return NotFound();

            var statusAnterior = solicitacao.Status;

            solicitacao.Status = "Aprovado";

            var historico = new HistoricoStatus
            {
                SolicitacaoId = solicitacao.Id,
                StatusAnterior = statusAnterior,
                StatusNovo = "Aprovado",
                UsuarioId = _userManager.GetUserId(User)!,
                DataAlteracao = DateTime.Now
            };

            _context.HistoricosStatus.Add(historico);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Aprovacoes));
        }

        // ==========================================
        // REJEITAR
        // ==========================================
        [Authorize(Roles = "Aprovador")]
        public async Task<IActionResult> Rejeitar(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);

            if (solicitacao == null)
                return NotFound();

            var statusAnterior = solicitacao.Status;

            solicitacao.Status = "Rejeitado";

            var historico = new HistoricoStatus
            {
                SolicitacaoId = solicitacao.Id,
                StatusAnterior = statusAnterior,
                StatusNovo = "Rejeitado",
                UsuarioId = _userManager.GetUserId(User)!,
                DataAlteracao = DateTime.Now
            };

            _context.HistoricosStatus.Add(historico);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Aprovacoes));
        }

        // ==========================================
        // DETALHES + HISTÓRICO
        // ==========================================
        public async Task<IActionResult> Detalhes(int id)
        {
            var solicitacao = await _context.Solicitacoes
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitacao == null)
                return NotFound();

            var historico = await _context.HistoricosStatus
                .Where(h => h.SolicitacaoId == id)
                .OrderByDescending(h => h.DataAlteracao)
                .ToListAsync();

            ViewBag.Historico = historico;

            return View(solicitacao);
        }
    }
}