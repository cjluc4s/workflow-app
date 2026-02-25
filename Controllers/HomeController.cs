using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkflowApp.Data;
using WorkflowApp.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel();

        if (User.Identity!.IsAuthenticated)
        {
            if (User.IsInRole("Aprovador"))
            {
                model.TotalGeral = await _context.Solicitacoes.CountAsync();
                model.TotalPendentes = await _context.Solicitacoes
                    .CountAsync(s => s.Status == "Pendente");

                model.TotalAprovadas = await _context.Solicitacoes
                    .CountAsync(s => s.Status == "Aprovado");

                model.TotalRejeitadas = await _context.Solicitacoes
                    .CountAsync(s => s.Status == "Rejeitado");
            }
            else
            {
                var userId = _userManager.GetUserId(User);

                model.TotalGeral = await _context.Solicitacoes
                    .CountAsync(s => s.UsuarioId == userId);

                model.TotalPendentes = await _context.Solicitacoes
                    .CountAsync(s => s.UsuarioId == userId && s.Status == "Pendente");

                model.TotalAprovadas = await _context.Solicitacoes
                    .CountAsync(s => s.UsuarioId == userId && s.Status == "Aprovado");

                model.TotalRejeitadas = await _context.Solicitacoes
                    .CountAsync(s => s.UsuarioId == userId && s.Status == "Rejeitado");
            }
        }

        return View(model);
    }
}