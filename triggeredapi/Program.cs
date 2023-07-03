using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using triggeredapi.Helpers;
using triggeredapi.Models;
using triggeredapi.Repo;
using triggeredapi.Service;
using triggeredapi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddIdentityCore<User>(o=> 
{   o.User.RequireUniqueEmail= false;
    o.Password.RequireDigit = false;
    o.Password.RequiredLength = 0;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;}).AddEntityFrameworkStores<DataContext>();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
// builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<AccessTokenGenerator>();
builder.Services.AddSingleton<NovelParser>();
builder.Services.AddDbContext<DataContext>(o=> o.UseSqlite(builder.Configuration.GetConnectionString("sqlite")));
builder.Services.AddScoped<TelegramMessageHandler>();
builder.Services.AddHostedService<TelegramService>();
builder.Services.AddAuthentication(x=>{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x=> {
    x.TokenValidationParameters = new TokenValidationParameters{
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"], 
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();    
    context.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
