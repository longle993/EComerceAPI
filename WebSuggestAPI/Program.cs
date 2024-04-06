using Microsoft.EntityFrameworkCore;
using WebSuggestAPI.Interface.Interface;
using WebSuggestAPI.Model.Model;
using WebSuggestAPI.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISanPhamRepository,SanPhamRepository>();
builder.Services.AddTransient<IHoaDonRepository, HoaDonRepository>();

builder.Services.AddDbContext<DbContextWebSuggest>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddControllers();



var app = builder.Build();

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
