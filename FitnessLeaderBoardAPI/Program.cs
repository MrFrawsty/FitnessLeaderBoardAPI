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

//builder.Configuration.AddSecretsManager(region: RegionEndpoint.USEast2, configurator: options =>
//{
//    options.SecretFilter = entry => entry.Name.StartsWith($"{enviroment}_{appName}_");
//    options.KeyGenerator = (_, s) => s.Replace($"{enviroment}_{appName}_", string.Empty)
//    .Replace("__", ":");    
//});
//builder.Configuration.AddAmazonSecretsManager("us-east-2", "FitnessLeaderboard-API-Key");
//builder.Host.ConfigureAppConfiguration(((_, configurationBuilder) =>
//{
//    configurationBuilder.AddAmazonSecretsManager("<your region>", "<secret name>");
//}));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.Configure<APICredentials>(builder.Configuration);

builder.Services.AddDbContext<FitnessLeaderboardDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FitnessLeaderboard"))); 

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseCors();
//what is useAuthorization
app.UseAuthentication();

app.MapControllers();

app.Run();
