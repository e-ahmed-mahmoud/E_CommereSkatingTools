using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.DataSeed;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options => options.AddPolicy("DefaultPolicy", p =>
     p.WithOrigins(["http://localhost:4200", "https://localhost:4200"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Defualt"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("DefaultPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

try
{
    using var serviceScope = app.Services.CreateScope();
    var storeDbContext = serviceScope.ServiceProvider.GetRequiredService<StoreDbContext>();
    await storeDbContext.Database.MigrateAsync();
    await DataSeed.SeedingData(storeDbContext);
}
catch (Exception ex)
{
    System.Console.WriteLine(ex);
}

app.Run();
