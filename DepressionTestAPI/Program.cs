using DepressionTestLib.Data;
using DepressionTestLib.DBContext;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<FeedbackManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Access-Control-Allow-Origin", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
    options.AddPolicy("CorsPolicy",
 builder => builder.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
    //options.AddPolicy("SiteCorsPolicy", builder => builder.AllowAnyOrigin()
    // .AllowAnyMethod()
    // .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseCors("Access-Control-Allow-Origin");
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseAuthentication();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtIssuer"],
        ValidAudience = configuration["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]))
    };
});
