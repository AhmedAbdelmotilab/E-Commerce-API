using E_Commerce.Domain.Entities.ProductModule ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace E_Commerce.Persistence.Data.Configurations ;

public class ProductConfiguration : IEntityTypeConfiguration < Product >
{
    public void Configure ( EntityTypeBuilder < Product > builder )
    {
        builder.Property ( Product => Product.Name ).HasColumnType ( "nvarchar" ).HasMaxLength ( 100 ) ;
        builder.Property ( Product => Product.Description ).HasColumnType ( "nvarchar" ).HasMaxLength ( 500 ) ;
        builder.Property ( Product => Product.PictureUrl ).HasColumnType ( "nvarchar" ).HasMaxLength ( 200 ) ;
        builder.Property ( Product => Product.Price ).HasPrecision ( 18 , 2 ) ;

        #region Configrution Of The Relationships

        builder.HasOne ( Product => Product.ProductBrand ).WithMany ( ).HasForeignKey ( Product => Product.BrandId ) ;
        builder.HasOne ( Product => Product.ProductType ).WithMany ( ).HasForeignKey ( Product => Product.TypeId ) ;

        #endregion
    }
}