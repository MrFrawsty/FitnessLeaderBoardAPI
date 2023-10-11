using Amazon;
using FitnessLeaderBoardAPI.Authentication;
using FitnessLeaderBoardAPI.Data;
using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var AllowedOrigins = "allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true);



        });
});

var enviroment = builder.Environment.EnvironmentName;
var appName = builder.Environment.ApplicationName;


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.Configure<APICredentials>(builder.Configuration);


builder.Services.AddDbContext<FitnessLeaderboardDbContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable(AuthConstants.DbConnectionString)));

var app = builder.Build();


if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseCors();

app.UseAuthentication();

app.MapControllers();

app.Run();
