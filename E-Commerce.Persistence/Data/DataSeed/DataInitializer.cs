using System.Text.Json ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Domain.Entities.SharedModule ;
using E_Commerce.Persistence.Data.DbContexts ;
using Microsoft.EntityFrameworkCore ;

namespace E_Commerce.Persistence.Data.DataSeed ;

public class DataInitializer : IDataInitializer
{
    private readonly StoreDbContext _dbContext ;

    public DataInitializer ( StoreDbContext dbContext )
    {
        _dbContext = dbContext ;
    }

    public void Initialize ( )
    {
        try
        {
            #region Check On That We Have Products and Brand and Types If Yes Go Out

            var HasProducts = _dbContext.Products.Any ( ) ;
            var HasProductBrands = _dbContext.ProductBrands.Any ( ) ;
            var HasProductTypes = _dbContext.ProductTypes.Any ( ) ;
            if ( HasProducts && HasProductBrands && HasProductTypes ) return ;

            #endregion

            #region Check On The ProductBrands If We Do Not Have ProductBrands Then Seed Data To The Table

            if ( ! HasProductBrands )
            {
                SeedDataFromJson < ProductBrand , int > ( "brands.json" , _dbContext.ProductBrands ) ;
            }

            #endregion

            #region Check On The ProductTypes If We Do Not Have ProductTypes Then Seed Data To The Table

            if ( ! HasProductTypes )
            {
                SeedDataFromJson < ProductType , int > ( "types.json" , _dbContext.ProductTypes ) ;
                _dbContext.SaveChanges ( ) ;
            }

            #endregion

            #region Check On The Products If We Do Not Have Products Then Seed Data To The Table

            if ( ! HasProducts )
            {
                SeedDataFromJson < Product , int > ( "products.json" , _dbContext.Products ) ;
                _dbContext.SaveChanges ( ) ;
            }

            #endregion
        }
        catch ( Exception e )
        {
            Console.WriteLine ( $"Data Seeding Failed {e}" ) ;
        }
    }

    #region Seed Data From JSON File To The Three Tables

    private void SeedDataFromJson < T , TKey > ( string fileName , DbSet < T > dbSet ) where T : BaseEntity < TKey >
    {
        var FilePath = @"..\E-Commerce.Persistence\Data\DataSeed\Files\" + fileName ;
        if ( ! File.Exists ( FilePath ) ) throw new FileNotFoundException ( $"File : {fileName} Not Found" ) ;
        try
        {
            using var DataStream = File.OpenRead ( FilePath ) ;
            var Data = JsonSerializer.Deserialize < List < T > > ( DataStream ,
                new JsonSerializerOptions ( )
                {
                    PropertyNameCaseInsensitive = true
                } ) ;
            if ( Data is not null )
            {
                dbSet.AddRange ( Data ) ;
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine ( $"Failed to read Data from file.{e}" ) ;
        }
    }

    #endregion
}