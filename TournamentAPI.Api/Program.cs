using Microsoft.EntityFrameworkCore;
using TournamentAPI.Api.Extensions;
using TournamentAPI.Data.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddDbContext<TournamentAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentContext")
    ?? throw new InvalidOperationException("Connection string 'TournamentContext' not found.")));

builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.SeedDataAsync();

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
