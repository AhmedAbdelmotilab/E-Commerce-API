using System.IdentityModel.Tokens.Jwt ;
using System.Security.Claims ;
using System.Text ;
using E_Commerce.Domain.Entities.IdentityModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.IdentityDTOs ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.Extensions.Configuration ;
using Microsoft.IdentityModel.Tokens ;

namespace E_Commerce.Services.Services ;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration ;
    private readonly UserManager < ApplicationUser > _userManager ;

    public AuthenticationService ( UserManager < ApplicationUser > userManager , IConfiguration configuration )
    {
        _userManager = userManager ;
        _configuration = configuration ;
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

    #region Method For Generate Token

    private async Task < string > CreateTokenAsync ( ApplicationUser user )
    {
        var Claims = new List < Claim > ( )
        {
            new Claim ( JwtRegisteredClaimNames.Email , user.Email! ) ,
            new Claim ( JwtRegisteredClaimNames.Name , user.UserName! ) ,
        } ;
        var Roles = await _userManager.GetRolesAsync ( user ) ;
        foreach ( var Role in Roles )
        {
            Claims.Add ( new Claim ( ClaimTypes.Role , Role ) ) ;
        }

        var SecretKey = _configuration [ "JWTOptions:SecretKey" ] ;
        var Key = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( SecretKey! ) ) ;
        var Cred = new SigningCredentials ( Key , SecurityAlgorithms.HmacSha256 ) ;
        var Token = new JwtSecurityToken (
            issuer : _configuration [ "JWTOptions:Issuer" ] ,
            audience : _configuration [ "JWTOptions:Audience" ] ,
            claims : Claims ,
            expires : DateTime.UtcNow.AddHours ( 1 ) ,
            signingCredentials : Cred
        ) ;
        return new JwtSecurityTokenHandler ( ).WriteToken ( Token ) ;
    }

    #endregion
}