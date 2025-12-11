using System.Text ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.IdentityModule ;
using E_Commerce.Persistence.Data.DataSeed ;
using E_Commerce.Persistence.Data.DbContexts ;
using E_Commerce.Persistence.IdentityData.Data.DataSeed ;
using E_Commerce.Persistence.IdentityData.Data.DbContexts ;
using E_Commerce.Persistence.Repositories ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.MappingProfile ;
using E_Commerce.Services.Services ;
using E_Commerce.Web.Extensions ;
using E_Commerce.Web.Factories ;
using E_Commerce.Web.Middlewares ;
using Microsoft.AspNetCore.Authentication.JwtBearer ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.IdentityModel.Tokens ;
using StackExchange.Redis ;

namespace E_Commerce.Web ;

public class Program
{
    public static async Task Main ( string [ ] args )
    {
        var builder = WebApplication.CreateBuilder ( args ) ; /* Create the builder */

        #region Add services to the container.

        builder.Services.AddControllers ( ) ; /* API Controllers */
        builder.Services.AddEndpointsApiExplorer ( ) ; /* Swagger */
        builder.Services.AddSwaggerGen ( ) ; /* Swagger */
        builder.Services.AddDbContext < StoreDbContext > ( options =>
        {
            options.UseSqlServer ( builder.Configuration.GetConnectionString ( "DefaultConnection" ) ) ;
        } ) ;
        builder.Services.AddKeyedScoped < IDataInitializer , DataInitializer > ( "Default" ) ;
        builder.Services.AddKeyedScoped < IDataInitializer , IdentityDataInitializer > ( "Identity" ) ;
        builder.Services.AddScoped < IUnitOfWork , UnitOfWork > ( ) ;
        builder.Services.AddSingleton < IConnectionMultiplexer > ( Sp =>
        {
            return ConnectionMultiplexer.Connect ( builder.Configuration.GetConnectionString ( "RedisConnection" )! ) ;
        } ) ;
        builder.Services.AddScoped < IBasketRepository , BasketRepository > ( ) ;
        builder.Services.AddScoped < IBasketService , BasketService > ( ) ;
        builder.Services.AddScoped < ICacheRepository , CacheRepository > ( ) ;
        builder.Services.AddScoped < ICacheService , CacheService > ( ) ;
        builder.Services.AddDbContext < StoreIdentityDbContext > ( options =>
        {
            options.UseSqlServer ( builder.Configuration.GetConnectionString ( "IdentityConnection" ) ) ;
        } ) ;
        builder.Services.AddIdentityCore < ApplicationUser > ( )
            .AddRoles < IdentityRole > ( )
            .AddEntityFrameworkStores < StoreIdentityDbContext > ( ) ;
        builder.Services.AddScoped < IAuthenticationService , AuthenticationService > ( ) ;
        builder.Services.AddAuthentication ( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme ;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme ;
            } )
            .AddJwtBearer ( options =>
            {
                options.SaveToken = true ;
                options.TokenValidationParameters = new TokenValidationParameters ( )
                {
                    ValidateIssuer = true ,
                    ValidateAudience = true ,
                    ValidateLifetime = true ,
                    ValidIssuer = builder.Configuration [ "JWTOptions:Issuer" ] ,
                    ValidAudience = builder.Configuration [ "JWTOptions:Audience" ] ,
                    IssuerSigningKey =
                        new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( builder.Configuration [ "JWTOptions:SecretKey" ] ! ) )
                } ;
            } ) ;

        #region Configure The API Controller Service

        builder.Services.Configure < ApiBehaviorOptions > ( options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse ;
            }
        ) ;

        #endregion

        #region With AutoMapper 15.0.0

        // builder.Services.AddAutoMapper ( X => X.AddProfile < ProductProfile > ( ) ) ;
        // builder.Services.AddTransient < ProductPictureUrlResolver > ( ) ;

        #endregion

        #region With AutoMapper 14.0.0

        builder.Services.AddAutoMapper ( typeof ( ProductProfile ).Assembly ) ;
        builder.Services.AddAutoMapper ( typeof ( OrderProfile ).Assembly ) ;

        #endregion

        builder.Services.AddScoped < IProductService , ProductService > ( ) ;
        builder.Services.AddScoped < IOrderService , OrderService > ( ) ;

        #endregion

        var app = builder.Build ( ) ; /* Build the app */

        #region Data Seeding

        await app.MigrationDatabaseAsync ( ) ;
        await app.MigrationIdentityDatabaseAsync ( ) ;
        await app.SeedDatabaseAsync ( ) ;
        await app.SeedIdentityDatabaseAsync ( ) ;

        #endregion

        #region Configure the HTTP request pipeline. Middleware.

        /* Handler Exceptions Middleware */
        app.UseMiddleware < ExceptionHandlerMiddleware > ( ) ;

        /* Swagger in Development */
        if ( app.Environment.IsDevelopment ( ) )
        {
            app.UseSwagger ( ) ; /* Swagger */
            app.UseSwaggerUI ( ) ; /* Swagger UI */
        }

        app.UseStaticFiles ( ) ;
        app.UseHttpsRedirection ( ) ; /* HTTPS Redirection */
        app.UseAuthentication ( ) ;
        app.UseAuthorization ( ) ;
        app.MapControllers ( ) ; /* Map API Controllers */

        #endregion

        await app.RunAsync ( ) ; /* Run the app */
    }
}