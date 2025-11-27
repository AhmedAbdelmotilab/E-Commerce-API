using System.Text ;
using E_Commerce.Services_Abstraction ;
using Microsoft.AspNetCore.Http ;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.AspNetCore.Mvc.Filters ;
using Microsoft.Extensions.DependencyInjection ;

namespace E_Commerce.Presentation.Attributes ;

public class RedisCacheAttribute : ActionFilterAttribute
{
    private readonly int _durationInMinutes ;

    public RedisCacheAttribute ( int DurationInMinutes = 5 )
    {
        _durationInMinutes = DurationInMinutes ;
    }

    public override async Task OnActionExecutionAsync ( ActionExecutingContext context , ActionExecutionDelegate next )
    {
        // 1. Get Cache Service From DI Container
        var CacheService = context.HttpContext.RequestServices.GetRequiredService < ICacheService > ( ) ;
        // Create CacheKey Based on Path and QueryString
        var CacheKey = CreateCacheKey ( context.HttpContext.Request ) ;
        // 2. Check If Cache Data Exists Return Cache Data And Skip The Executing Of The EndPoint
        var CacheValue = await CacheService.GetAsync ( CacheKey ) ;
        if ( CacheValue is not null )
        {
            context.Result = new ContentResult ( )
            {
                Content = CacheValue ,
                ContentType = "application/Json" ,
                StatusCode = StatusCodes.Status200OK ,
            } ;
            return ;
        }

        // 3. If Cache Not Exits Then Executing The EndPoint And If Result Is 200 Ok The Store The Data In Cache
        var ExecutedContext = await next.Invoke ( ) ;
        if ( ExecutedContext.Result is OkObjectResult result )
        {
            await CacheService.SetAsync ( CacheKey , result.Value! , TimeSpan.FromMinutes ( _durationInMinutes ) ) ;
        }
    }

    private string CreateCacheKey ( HttpRequest request )
    {
        StringBuilder Key = new StringBuilder ( ) ;
        Key.Append ( request.Path ) ; // BaseUrl/api/Products|
        foreach ( var item in request.Query.OrderBy ( X => X.Key ) )
        {
            Key.Append ( $"|{item.Key}-{item.Value}" ) ; // BaseUrl/api/Products|brandId-2|typeId-1
        }

        return Key.ToString ( ) ;
    }
}