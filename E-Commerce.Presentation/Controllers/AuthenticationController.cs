using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.DTOs.IdentityDTOs ;
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
}