using FitnessLeaderBoardAPI.Authentication;
using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("FitnessLeaderboard") ?? "Data Source=FitnessLeaderboardDbContext.db";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddDbContext<FitnessLeaderboardDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FitnessLeaderboard"))); 
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthentication();

app.UseCors(options => options.AllowAnyOrigin());

app.MapControllers();

app.Run();
