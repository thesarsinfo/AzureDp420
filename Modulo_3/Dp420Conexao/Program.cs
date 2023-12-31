using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Repository;
using Dp420Conexao.Repository.Interfaces;
using Dp420Conexao.Service;
using Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.Configure<CosmosDbSettings>(configuration.GetSection("CosmosDbSettings"));
builder.Services.AddSingleton<ICosmosNoSQLContext,CosmosNoSQLContext>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();
builder.Services.AddSingleton<ILogLoginRepository, LogLoginRepository>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<LogLoginService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
CosmosNoSQLContext context = new(configuration);
await context.IniciadorCosmo();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
