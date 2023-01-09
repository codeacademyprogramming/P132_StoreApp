using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Api.Admin.Dtos.CategoryDtos;
using Store.Api.Admin.Profiles;
using Store.Core.Entities;
using Store.Data.DAL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidator>());

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<StoreDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cd Data layer
//dotnet ef --startup-project ..\Store.Api migrations add 
//dotnet ef --startup-project ..\Store.Api database update 

builder.Services.AddDbContext<StoreDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile(new AdminMapper());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
