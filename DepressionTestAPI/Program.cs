using DepressionTestLib.Data;
using DepressionTestLib.DBContext;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<DepressionTestDBContext>(option =>
{
    option.UseSqlServer(configuration.GetConnectionString("DepressionConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>()
      .AddEntityFrameworkStores<DepressionTestDBContext>()
      .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<DepressionTestManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
