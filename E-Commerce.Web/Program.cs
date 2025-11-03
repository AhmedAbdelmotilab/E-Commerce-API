using E_Commerce.Domain.Contracts ;
using E_Commerce.Persistence.Data.DataSeed ;
using E_Commerce.Persistence.Data.DbContexts ;
using E_Commerce.Web.Extensions ;
using Microsoft.EntityFrameworkCore ;

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

        app.UseHttpsRedirection ( ) ; /* HTTPS Redirection */

        app.MapControllers ( ) ; /* Map API Controllers */

        #endregion

        await app.RunAsync ( ) ; /* Run the app */
    }
}