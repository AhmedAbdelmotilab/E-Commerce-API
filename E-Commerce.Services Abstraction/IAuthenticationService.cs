using E_Commerce.Shared.CommonResult ;
using E_Commerce.Shared.DTOs.IdentityDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IAuthenticationService
{
    // Login => Get Email and Password => Return Result With Token And Display Name , Email 
    Task < Result < UserDto > > LoginAsync ( LoginDto loginDto ) ;

    // Register => Get Email,Password,UserName,DisplayName,PhoneNumber => Return Result With Token And Display Name , Email 
    Task < Result < UserDto > > RegisterAsync ( RegisterDto registerDto ) ;
}