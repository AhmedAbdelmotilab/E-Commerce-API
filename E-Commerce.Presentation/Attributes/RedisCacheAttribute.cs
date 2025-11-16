using Microsoft.AspNetCore.Mvc.Filters ;

namespace E_Commerce.Presentation.Attributes ;

public class RedisCacheAttribute : ActionFilterAttribute
{
    public override Task OnActionExecutionAsync ( ActionExecutingContext context , ActionExecutionDelegate next )
    {
        // 1. Get Cache Service From DI Container
        // 2. Check If Cache Data Exists Return Cache Data And Skip The Executing Of The EndPoint
        // 3. If Cache Not Exits Then Executing The EndPoint And If Result Is 200 Ok The Store The Data In Cache
    }
}