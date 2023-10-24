using CatsyCatStudio;
using DAL.Database;
using DAL.Interfaces;
using DAL.Repositeries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Api jwt token verification challenges.
builder.Services.AddJwtValidation(Configuration);

// Add swagger with definitions
builder.Services.AddSwaggerImpliment();

// connection with sql server
builder.Services.AddDbContext<CatsyCatDbContext>(dbOptions => dbOptions.UseSqlServer(builder.Configuration["DefaultConnection:ConnectionString"]));

// register application services
builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddTransient<ICustomerService, CustomerServices>();
builder.Services.AddTransient<IProductService, ProductServices>();
builder.Services.AddTransient<IOrderService, OrderServices>();
builder.Services.AddTransient<IOrderProductsServices, OrderProductsServices>();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();