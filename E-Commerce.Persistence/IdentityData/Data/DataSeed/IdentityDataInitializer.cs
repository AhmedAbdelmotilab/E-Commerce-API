using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.IdentityModule ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.Extensions.Logging ;

namespace E_Commerce.Persistence.IdentityData.Data.DataSeed ;

public class IdentityDataInitializer : IDataInitializer
{
    private readonly ILogger < IdentityDataInitializer > _logger ;
    private readonly RoleManager < IdentityRole > _roleManager ;
    private readonly UserManager < ApplicationUser > _userManager ;

    public IdentityDataInitializer (
        RoleManager < IdentityRole > roleManager ,
        UserManager < ApplicationUser > userManager ,
        ILogger < IdentityDataInitializer > logger )
    {
        _roleManager = roleManager ;
        _userManager = userManager ;
        _logger = logger ;
    }

    public async Task InitializeAsync ( )
    {
        try
        {
            if ( ! _roleManager.Roles.Any ( ) )
            {
                await _roleManager.CreateAsync ( new IdentityRole ( "Admin" ) ) ;
                await _roleManager.CreateAsync ( new IdentityRole ( "SuperAdmin" ) ) ;
            }

            if ( ! _userManager.Users.Any ( ) )
            {
                var User01 = new ApplicationUser ( )
                {
                    DisplayName = "Ahmed Abdelmotilab" ,
                    UserName = "AhmedAbdelmotilab" ,
                    Email = "A.Abdelmotilab@gmail.com" ,
                    PhoneNumber = "01553130965"
                } ;
                var User02 = new ApplicationUser ( )
                {
                    DisplayName = "Osama Ali" ,
                    UserName = "OsamaAli" ,
                    Email = "OsamaAli@gmail.com" ,
                    PhoneNumber = "01117028828"
                } ;
                await _userManager.CreateAsync ( User01 , "P@ssw0rd" ) ;
                await _userManager.AddToRoleAsync ( User01 , "Admin" ) ;
                await _userManager.CreateAsync ( User02 , "P@ssw0rd" ) ;
                await _userManager.AddToRoleAsync ( User02 , "SuperAdmin" ) ;
            }
        }
        catch ( Exception ex )
        {
            _logger.LogError ( ex.Message , "An error occured during initialization" ) ;
        }
    }
}