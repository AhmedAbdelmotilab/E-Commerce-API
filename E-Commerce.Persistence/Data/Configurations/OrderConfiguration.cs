using E_Commerce.Domain.Entities.OrderModule ;
using Microsoft.EntityFrameworkCore ;
using Microsoft.EntityFrameworkCore.Metadata.Builders ;

namespace E_Commerce.Persistence.Data.Configurations ;

public class OrderConfiguration : IEntityTypeConfiguration < Order >
{
    public void Configure ( EntityTypeBuilder < Order > builder )
    {
        builder.Property ( X => X.SubTotal ).HasPrecision ( 8 , 2 ) ;
        builder.OwnsOne ( X => X.Address ,
            OEntity =>
            {
                OEntity.Property ( O => O.FirstName ).HasMaxLength ( 50 ) ;
                OEntity.Property ( O => O.LastName ).HasMaxLength ( 50 ) ;
                OEntity.Property ( O => O.Street ).HasMaxLength ( 50 ) ;
                OEntity.Property ( O => O.City ).HasMaxLength ( 50 ) ;
                OEntity.Property ( O => O.Country ).HasMaxLength ( 50 ) ;
            }
        ) ;
    }
}