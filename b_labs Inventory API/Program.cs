using AutoMapper;
using b_labs_Inventory_API.Mapper;
using Core;
using Core.Interfaces;
using Dal;
using Dal.Interfaces;
using Entites;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<IInventoryContext, InventoryContext>();
string mySQLConnectionString = configuration.GetConnectionString("InventoryDB");
services.AddDbContext<InventoryContext>(options => options.UseMySql(mySQLConnectionString,
    ServerVersion.AutoDetect(mySQLConnectionString)));

#region AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile(configuration));
});
services.AddSingleton(mappingConfig.CreateMapper());
#endregion


#region Inject Core Interfaces
services.AddScoped<IProductCore, ProductCore>();
services.AddScoped<ICategoryCore, CategoryCore>();
#endregion

#region Inject Dal Interfaces
services.AddScoped<IProductDal, ProductDal>();
services.AddScoped<ICategoryDal, CategoryDal>();
services.AddScoped<IProductRatingDal, ProductRatingDal>();
services.AddScoped<IUserSearchHistoryDal, UserSearchHistoryDal>();
#endregion

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
