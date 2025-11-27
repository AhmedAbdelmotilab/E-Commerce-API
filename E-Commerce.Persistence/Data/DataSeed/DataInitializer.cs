using System.Text.Json ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.OrderModule ;
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

    public async Task InitializeAsync ( )
    {
        try
        {
            #region Check On That We Have Products and Brand and Types and DeliveryMethods If Yes Go Out

            var HasProducts = await _dbContext.Products.AnyAsync ( ) ;
            var HasProductBrands = await _dbContext.ProductBrands.AnyAsync ( ) ;
            var HasProductTypes = await _dbContext.ProductTypes.AnyAsync ( ) ;
            var HasDeliveryMethods = await _dbContext.Set < DeliveryMethod > ( ).AnyAsync ( ) ;
            if ( HasProducts && HasProductBrands && HasProductTypes && HasDeliveryMethods ) return ;

            #endregion

            #region Check On The ProductBrands If We Do Not Have ProductBrands Then Seed Data To The Table

            if ( ! HasProductBrands )
            {
                await SeedDataFromJsonAsync < ProductBrand , int > ( "brands.json" , _dbContext.ProductBrands ) ;
            }

            #endregion

            #region Check On The ProductTypes If We Do Not Have ProductTypes Then Seed Data To The Table

            if ( ! HasProductTypes )
            {
                await SeedDataFromJsonAsync < ProductType , int > ( "types.json" , _dbContext.ProductTypes ) ;
                await _dbContext.SaveChangesAsync ( ) ;
            }

            #endregion

            #region Check On The Products If We Do Not Have Products Then Seed Data To The Table

            if ( ! HasProducts )
            {
                await SeedDataFromJsonAsync < Product , int > ( "products.json" , _dbContext.Products ) ;
            }

            #endregion

            #region Check On The DeliveryMethods If We Do Not Have DeliveryMethods Then Seed Data To The Table

            if ( ! HasDeliveryMethods )
            {
                await SeedDataFromJsonAsync < DeliveryMethod , int > ( "delivery.json" , _dbContext.Set < DeliveryMethod > ( ) ) ;
            }

            #endregion

            await _dbContext.SaveChangesAsync ( ) ;
        }
        catch ( Exception e )
        {
            Console.WriteLine ( $"Data Seeding Failed {e}" ) ;
        }
    }

    #region Seed Data From JSON File To The Three Tables

    private async Task SeedDataFromJsonAsync < T , TKey > ( string fileName , DbSet < T > dbSet )
        where T : BaseEntity < TKey >
    {
        var FilePath = @"..\E-Commerce.Persistence\Data\DataSeed\Files\" + fileName ;
        if ( ! File.Exists ( FilePath ) ) throw new FileNotFoundException ( $"File : {fileName} Not Found" ) ;
        try
        {
            using var DataStream = File.OpenRead ( FilePath ) ;
            var Data = await JsonSerializer.DeserializeAsync < List < T > > ( DataStream ,
                new JsonSerializerOptions ( )
                {
                    PropertyNameCaseInsensitive = true
                } ) ;
            if ( Data is not null )
            {
                await dbSet.AddRangeAsync ( Data ) ;
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine ( $"Failed to read Data from file.{e}" ) ;
        }
    }

    #endregion
}