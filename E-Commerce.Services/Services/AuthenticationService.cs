using E_Commerce.Domain.Entities.IdentityModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.IdentityDTOs ;
using Microsoft.AspNetCore.Identity ;

namespace E_Commerce.Services.Services ;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager < ApplicationUser > _userManager ;

    public AuthenticationService ( UserManager < ApplicationUser > userManager )
    {
        _userManager = userManager ;
    }

    public async Task < Result < UserDto > > LoginAsync ( LoginDto loginDto )
    {
        var User = await _userManager.FindByEmailAsync ( loginDto.Email ) ;
        if ( User is null )
        {
            return Error.InvalidCredentials ( "User Invalid Credentials" ) ;
        }

        var IsPasswordValid = await _userManager.CheckPasswordAsync ( User , loginDto.Password ) ;
        if ( ! IsPasswordValid )
        {
            return Error.InvalidCredentials ( "User Invalid Credentials" ) ;
        }

        return new UserDto ( User.Email! , User.DisplayName , "Token" ) ;
    }

    public async Task < Result < UserDto > > RegisterAsync ( RegisterDto registerDto )
    {
        var User = new ApplicationUser ( )
        {
            Email = registerDto.Email ,
            DisplayName = registerDto.DisplayName ,
            PhoneNumber = registerDto.PhoneNumber ,
            UserName = registerDto.UserName ,
        } ;
        var IdentityResult = await _userManager.CreateAsync ( User , registerDto.Password ) ;
        if ( IdentityResult.Succeeded )
        {
            return new UserDto ( User.Email , User.DisplayName , "Token" ) ;
        }
        else
        {
            return IdentityResult.Errors.Select ( E => Error.Validation ( E.Code , E.Description ) ).ToList ( ) ;
        }
    }
}