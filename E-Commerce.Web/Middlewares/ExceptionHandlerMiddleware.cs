using E_Commerce.Services.Exceptions ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Web.Middlewares ;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger < ExceptionHandlerMiddleware > _logger ;
    private readonly RequestDelegate _next ;

    public ExceptionHandlerMiddleware ( RequestDelegate next , ILogger < ExceptionHandlerMiddleware > logger )
    {
        _next = next ;
        _logger = logger ;
    }

    public async Task InvokeAsync ( HttpContext httpContext )
    {
        try
        {
            await _next.Invoke ( httpContext ) ;
            await HandelNotFoundEndPoint ( httpContext ) ;
        }
        catch ( Exception ex )
        {
            _logger.LogError ( ex , $"Something went wrong : {ex.Message}" ) ;
            var Problem = new ProblemDetails ( )
            {
                Title = "An Unexpected Error has Occurred" ,
                Status = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound ,
                    _ => StatusCodes.Status500InternalServerError
                } ,
                Detail = ex.Message ,
                Instance = httpContext.Request.Path ,
            } ;
            httpContext.Response.StatusCode = Problem.Status.Value ;
            await httpContext.Response.WriteAsJsonAsync ( Problem ) ;
        }
    }

    private static async Task HandelNotFoundEndPoint ( HttpContext httpContext )
    {
        if ( httpContext.Response.StatusCode == StatusCodes.Status404NotFound )
        {
            var Problem = new ProblemDetails ( )
            {
                Title = "Error While Processing Request - EndPoint Not Found" ,
                Status = StatusCodes.Status404NotFound ,
                Detail = $"EnPoint {httpContext.Request.Path} Not Found" ,
                Instance = httpContext.Request.Path ,
            } ;
            await httpContext.Response.WriteAsJsonAsync ( Problem ) ;
        }
    }
}