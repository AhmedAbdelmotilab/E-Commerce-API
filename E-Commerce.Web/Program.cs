using E_Commerce.Domain.Contracts ;
using E_Commerce.Persistence.Data.DataSeed ;
using E_Commerce.Persistence.Data.DbContexts ;
using E_Commerce.Persistence.Repositories ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.MappingProfile ;
using E_Commerce.Services.Services ;
using E_Commerce.Web.Extensions ;
using Microsoft.EntityFrameworkCore ;
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
        builder.Services.AddScoped < IDataInitializer , DataInitializer > ( ) ;
        builder.Services.AddScoped < IUnitOfWork , UnitOfWork > ( ) ;
        builder.Services.AddSingleton < IConnectionMultiplexer > ( Sp =>
        {
            return ConnectionMultiplexer.Connect ( builder.Configuration.GetConnectionString ( "RedisConnection" )! ) ;
        } ) ;
        builder.Services.AddScoped < IBasketRepository , BasketRepository > ( ) ;
        builder.Services.AddScoped < IBasketService , BasketService > ( ) ;
        builder.Services.AddScoped < ICacheRepository , CacheRepository > ( ) ;

        #region With AutoMapper 15.0.0

        // builder.Services.AddAutoMapper ( X => X.AddProfile < ProductProfile > ( ) ) ;
        // builder.Services.AddTransient < ProductPictureUrlResolver > ( ) ;

        #endregion

        #region With AutoMapper 14.0.0

        builder.Services.AddAutoMapper ( typeof ( ProductProfile ).Assembly ) ;

        #endregion

        builder.Services.AddScoped < IProductService , ProductService > ( ) ;

        #endregion

        var app = builder.Build ( ) ; /* Build the app */

        #region Data Seeding

        await app.MigrationDatabaseAsync ( ) ;
        await app.SeedDatabaseAsync ( ) ;

        #endregion

        #region Configure the HTTP request pipeline. Middleware.

        /* Swagger in Development */
        if ( app.Environment.IsDevelopment ( ) )
        {
            app.UseSwagger ( ) ; /* Swagger */
            app.UseSwaggerUI ( ) ; /* Swagger UI */
        }

        app.UseStaticFiles ( ) ;
        app.UseHttpsRedirection ( ) ; /* HTTPS Redirection */

        app.MapControllers ( ) ; /* Map API Controllers */

        #endregion

        await app.RunAsync ( ) ; /* Run the app */
    }
}