using E_Commerce.Domain.Entities.ProductModule ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace E_Commerce.Persistence.Data.Configurations ;

public class ProductTypeConfiguration : IEntityTypeConfiguration < ProductType >
{
    public void Configure ( EntityTypeBuilder < ProductType > builder )
    {
        builder.Property ( ProductType => ProductType.Name ).HasColumnType ( "nvarchar" ).HasMaxLength ( 100 ) ;
    }
}