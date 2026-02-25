using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkflowApp.Data;

var builder = WebApplication.CreateBuilder(args);

// =======================
// DbContext (SQLite)
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=workflow.db"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// =======================
// Identity com Roles
// =======================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// =======================
// Criar Roles e atribuir Aprovador
// =======================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Solicitante", "Aprovador" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"Role criada: {role}");
        }
    }

    // 🔥 COLOQUE SEU EMAIL EXATO AQUI
    var emailAprovador = "lucas.januario@cetsp.com.br";

    var user = await userManager.FindByEmailAsync(emailAprovador);

    if (user != null)
    {
        Console.WriteLine("Usuário encontrado.");

        if (!await userManager.IsInRoleAsync(user, "Aprovador"))
        {
            await userManager.AddToRoleAsync(user, "Aprovador");
            Console.WriteLine("Role Aprovador atribuída com sucesso!");
        }
        else
        {
            Console.WriteLine("Usuário já é Aprovador.");
        }
    }
    else
    {
        Console.WriteLine("Usuário NÃO encontrado.");
    }
}

// =======================
// Pipeline HTTP
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();