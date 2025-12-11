using System.Security.Claims ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.IdentityDTOs ;
using Microsoft.AspNetCore.Authorization ;
using Microsoft.AspNetCore.Mvc ;

namespace E_Commerce.Presentation.Controllers ;

public class AuthenticationController : ApiBaseController
{
    private readonly IAuthenticationService _authenticationService ;

    public AuthenticationController ( IAuthenticationService authenticationService )
    {
        _authenticationService = authenticationService ;
    }

    // LOGIN : BaseUrl/api/Authentication/Login
    [ HttpPost ( "Login" ) ]
    public async Task < ActionResult < UserDto > > Login ( LoginDto loginDto )
    {
        var Result = await _authenticationService.LoginAsync ( loginDto ) ;
        return HandelResult ( Result ) ;
    }

    // REGISTER : BaseUrl/api/Authentication/Register
    [ HttpPost ( "Register" ) ]
    public async Task < ActionResult < UserDto > > Register ( RegisterDto registerDto )
    {
        var Result = await _authenticationService.RegisterAsync ( registerDto ) ;
        return HandelResult ( Result ) ;
    }

    // CheckEmailExits : BaseUrl/api/Authentication/emailExits
    [ HttpGet ( "emailExists" ) ]
    public async Task < ActionResult < bool > > CheckEmail ( string email )
    {
        var Result = await _authenticationService.CheckEmailAsync ( email ) ;
        return Ok ( Result ) ;
    }

    // GetUserByEmail : BaseUrl/api/Authentication/CurrentUser
    [ Authorize ]
    [ HttpGet ( "CurrentUser" ) ]
    public async Task < ActionResult < UserDto > > GetCurrentUser ( )
    {
        var Email = User.FindFirstValue ( ClaimTypes.Email )! ;
        var Result = await _authenticationService.GetUserAsync ( Email ) ;
        return HandelResult ( Result ) ;
    }
}