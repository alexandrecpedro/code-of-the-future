using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto_Radar.AuthService;
using Projeto_Radar.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Projeto Radar",
        Description = "Desenvolvimento do projeto integrador Radar do grupo 4",
        Contact = new OpenApiContact { Name = "Grupo 4", Email = "" },
        License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT como no exemplo: Bearer {SEU_TOKEN}",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "corspolicy",
        builder =>
        {
            builder
            .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

// var conexao = builder.Configuration.GetConnectionString("conexao");
//var conexao = "Server=35.198.28.177;Database=radar;Uid=codfutdnet;Pwd=123qwe;";
//var conexao = "Uid=root;Password=e88luHuzCFuc7rdilwqL;Server=dbradar.up.railway.app:containers-us-west-196.railway.app;Port=6936;Database=railway;default command timeout=0;SslMode=none";
var conexao = "server=/cloudsql/desafio-final-dotnet:southamerica-east1:codigo-do-futuro;database=radar;uid=codfutdnet;pwd=123qwe;";
builder.Services.AddDbContext<DBContext>(options => options.UseMySql(conexao, ServerVersion.AutoDetect(conexao)));

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthTokenService.Key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adm", policy => policy.RequireClaim("adm"));
    options.AddPolicy("editor", policy => policy.RequireClaim("editor"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("corspolicy");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
