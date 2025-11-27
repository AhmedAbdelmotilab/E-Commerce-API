using E_Commerce.Domain.Entities.IdentityModule ;
using Microsoft.AspNetCore.Identity ;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore ;

namespace E_Commerce.Persistence.IdentityData.Data.DbContexts ;

public class StoreIdentityDbContext : IdentityDbContext < ApplicationUser >
{
    public StoreIdentityDbContext ( DbContextOptions < StoreIdentityDbContext > options ) : base ( options ) { }

    override protected void OnModelCreating ( ModelBuilder modelBuilder )
    {
        base.OnModelCreating ( modelBuilder ) ;
        modelBuilder.Entity < Address > ( ).ToTable ( "Addresses" ) ;
        modelBuilder.Entity < ApplicationUser > ( ).ToTable ( "Users" ) ;
        modelBuilder.Entity < IdentityRole > ( ).ToTable ( "Roles" ) ;
        modelBuilder.Entity < IdentityUserRole < string > > ( ).ToTable ( "UserRoles" ) ;
    }
}