namespace E_Commerce.Web ;

public class Program
{
    public static void Main ( string [ ] args )
    {
        var builder = WebApplication.CreateBuilder ( args ) ; /* Create the builder */

        #region Add services to the container.

        builder.Services.AddControllers ( ) ; /* API Controllers */
        builder.Services.AddEndpointsApiExplorer ( ) ; /* Swagger */
        builder.Services.AddSwaggerGen ( ) ; /* Swagger */

        #endregion

        var app = builder.Build ( ) ; /* Build the app */

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

        app.Run ( ) ; /* Run the app */
    }
}