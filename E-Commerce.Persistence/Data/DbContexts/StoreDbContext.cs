using System.Reflection ;
using E_Commerce.Domain.Entities.ProductModule ;
using Microsoft.EntityFrameworkCore ;

namespace E_Commerce.Persistence.Data.DbContexts ;

public class StoreDbContext : DbContext
{
    public StoreDbContext ( DbContextOptions < StoreDbContext > options ) : base ( options ) { }
    public DbSet < Product > Products { get ; set ; }
    public DbSet < ProductBrand > ProductBrands { get ; set ; }
    public DbSet < ProductType > ProductTypes { get ; set ; }

    protected override void OnModelCreating ( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfigurationsFromAssembly ( Assembly.GetExecutingAssembly ( ) ) ;
    }
}