using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Store.Api.Admin.Dtos.CategoryDtos;
using Store.Api.Admin.Profiles;
using Store.Api.Services;
using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Data.DAL;
using Store.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidator>());

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<StoreDbContext>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("admin", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StoreApp Admin Api",
        Version = "v1",
    });

    opt.SwaggerDoc("user", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StoreApp Client Api",
        Version = "v1",
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    opt.AddFluentValidationRules();
});

//cd Data layer
//dotnet ef --startup-project ..\Store.Api migrations add 
//dotnet ef --startup-project ..\Store.Api database update 

builder.Services.AddDbContext<StoreDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//builder.Services.AddAutoMapper(opt =>
//{
//    opt.AddProfile(new AdminMapper());
//});

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
  {
      cfg.AddProfile(new AdminMapper(provider.GetService<IHttpContextAccessor>()));
  }).CreateMapper());



builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; ;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; ;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidAudience = builder.Configuration.GetSection("JWT:audience").Value,
        ValidIssuer = builder.Configuration.GetSection("JWT:issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:secret").Value))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin Api");
        opt.SwaggerEndpoint("/swagger/user/swagger.json", "Client Api");

    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();



