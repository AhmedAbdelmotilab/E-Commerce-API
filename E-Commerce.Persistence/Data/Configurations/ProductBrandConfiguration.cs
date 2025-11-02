using E_Commerce.Domain.Entities.ProductModule ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace E_Commerce.Persistence.Data.Configurations ;

public class ProductBrandConfiguration : IEntityTypeConfiguration < ProductBrand >
{
    public void Configure ( EntityTypeBuilder < ProductBrand > builder )
    {
        builder.Property ( ProductBrand => ProductBrand.Name ).HasColumnType ( "nvarchar" ).HasMaxLength ( 100 ) ;
    }
}