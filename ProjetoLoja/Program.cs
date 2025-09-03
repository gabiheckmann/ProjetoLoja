using ProjetoLoja.Repositório;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. REGISTRAR OS SERVIÇOS DE SESSÃO
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de inatividade da sessão (pode ajustar)
    options.Cookie.HttpOnly = true; // Impede que o cookie seja acessado por scripts do lado do cliente
    options.Cookie.IsEssential = true; // Torna o cookie de sessão essencial para a funcionalidade do aplicativo
});

// REGISTRAR A CONNECTION STRING COMO UM SERVIÇO STRING AQUI
builder.Services.AddSingleton<string>(builder.Configuration.GetConnectionString("DefaultConnection")!);

// injetar repositórios
builder.Services.AddScoped<ProdutoRepositorio>();
builder.Services.AddScoped<CarrinhoRepositorio>();
builder.Services.AddScoped<PedidoRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();// autoriza navegação

// ESTA LINHA É CRUCIAL E DEVE VIR ANTES DE app.UseAuthorization() ou app.MapControllerRoute(), permite a sessão <<<
app.UseSession();

app.UseAuthorization(); // autorização da navegação

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
