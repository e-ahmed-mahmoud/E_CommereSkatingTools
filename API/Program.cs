using System.Reflection;
using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.DataSeed;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Services;
using Infrastructure.Identity;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options => options.AddPolicy("DefaultPolicy", p =>
     p.WithOrigins(["http://localhost:4200", "https://localhost:4200"]).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Defualt"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connectinString = builder.Configuration.GetConnectionString("Redis")
        ?? throw new ArgumentException("connection string not defined");
    var configuraiton = ConfigurationOptions.Parse(connectinString, true);
    return ConnectionMultiplexer.Connect(configuraiton);

});
builder.Services.AddSingleton<ICartService, CartService>();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<StoreDbContext>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IAccountService, AccountService>();

//mapster config
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(IAssemblyMarker).Assembly);
builder.Services.AddSingleton<IMapper>(new Mapper(config));

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("DefaultPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.UseHttpInfoLog();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<ApplicationUser>();


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
