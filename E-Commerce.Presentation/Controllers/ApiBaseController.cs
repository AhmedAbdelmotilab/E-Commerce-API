using System.Security.Claims ;
using E_Commerce.Shared.CommonResult ;
using Microsoft.AspNetCore.Http ;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.AspNetCore.Mvc.ModelBinding ;

namespace E_Commerce.Presentation.Controllers ;

[ ApiController ]
[ Route ( "api/[controller]" ) ]
public class ApiBaseController : ControllerBase
{
    // Handel Result Without Value
    // 1. If Result Is Success Return NoContent 204
    // 2. If Result Is Failure Return Problem With Status Code And Error Details 
    protected IActionResult HandelResult ( Result result )
    {
        if ( result.IsSuccess )
        {
            return NoContent ( ) ;
        }
        else
        {
            return HandelProblem ( result.Errors ) ;
        }
    }

    // Handel Result With Value
    // 1. If Result Is Success Return OK 200 With The Value
    // 2. If Result Is Failure Return Problem With Status Code And Error Details 
    protected ActionResult < TValue > HandelResult < TValue > ( Result < TValue > result )
    {
        if ( result.IsSuccess )
        {
            return Ok ( result.Value ) ;
        }
        else
        {
            return HandelProblem ( result.Errors ) ;
        }
    }

    // Handel Problem
    private ActionResult HandelProblem ( IReadOnlyList < Error > errors )
    {
        // If No Errors Are Provided , Return 500 Error
        if ( errors.Count == 0 )
        {
            return Problem ( statusCode : StatusCodes.Status500InternalServerError , title : "Unknown Error" ) ;
        }

        // If All Errors Are Validation Errors , Handel Them As Validation Problem
        if ( errors.All ( E => E.ErrorType == ErrorType.Validation ) )
        {
            return HandelValidationProblem ( errors ) ;
        }

        // If There is Only One Error , Handel It As Single Error Problem 
        return HandelSingleErrorProblem ( errors [ 0 ] ) ;
    }

    // If No Errors Are Provided , Return 500 Error
    private ActionResult HandelSingleErrorProblem ( Error error )
    {
        return Problem (
            title : error.Code ,
            detail : error.Description ,
            type : error.ErrorType.ToString ( ) ,
            statusCode : MapErrorTypeToStatusCode ( error.ErrorType )
        ) ;
    }

    private static int MapErrorTypeToStatusCode ( ErrorType errorType )
        => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound ,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized ,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden ,
            ErrorType.Validation => StatusCodes.Status400BadRequest ,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized ,
            ErrorType.Failure => StatusCodes.Status500InternalServerError ,
            _ => StatusCodes.Status500InternalServerError
        } ;

    private ActionResult HandelValidationProblem ( IReadOnlyList < Error > errors )
    {
        var ModelStateDictionary = new ModelStateDictionary ( ) ;
        foreach ( var error in errors )
        {
            ModelStateDictionary.AddModelError ( error.Code , error.Description ) ;
        }

        return ValidationProblem ( ModelStateDictionary ) ;
    }

    // Get User Email From Token
    protected string GetEmailFromToken ( ) => User.FindFirstValue ( ClaimTypes.Email )! ;
}