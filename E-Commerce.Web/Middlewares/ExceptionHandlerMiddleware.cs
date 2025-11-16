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
        }
        catch ( Exception ex )
        {
            _logger.LogError ( ex , $"Something went wrong : {ex.Message}" ) ;
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError ;
            var Problem = new ProblemDetails ( )
            {
                Title = "An Unexpected Error has Occurred" ,
                Status = StatusCodes.Status500InternalServerError ,
                Detail = ex.Message ,
                Instance = httpContext.Request.Path ,
            } ;
            await httpContext.Response.WriteAsJsonAsync ( Problem ) ;
        }
    }
}