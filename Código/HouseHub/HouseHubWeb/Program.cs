using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("HouseHubDatabase") ?? throw new InvalidOperationException();
builder.Services.AddDbContext<HouseHubContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddTransient<IAgendamentoService, AgendamentoService>();
builder.Services.AddTransient<IAvalicaoService, AvaliacaoService>();
builder.Services.AddTransient<IImovelService, ImovelService>();
builder.Services.AddTransient<IPessoaService, PessoaService>();
builder.Services.AddTransient<ISolicitacaoreparoService, SolicitacaoreparoService>();
builder.Services.AddTransient<ILocacaoService, LocacaoService>();
builder.Services.AddTransient<IPagamentoService, PagamentoService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

