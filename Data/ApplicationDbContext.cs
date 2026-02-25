using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkflowApp.Models;

namespace WorkflowApp.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =======================
        // DbSets do Sistema
        // =======================

        public DbSet<Solicitacao> Solicitacoes { get; set; } = default!;

        public DbSet<HistoricoStatus> HistoricosStatus { get; set; } = default!;
    }
}