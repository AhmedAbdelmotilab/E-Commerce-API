using E_Commerce.Domain.Entities.OrderModule ;

namespace E_Commerce.Services.Specifications ;

public class OrderSpecification : BaseSpecification < Order , Guid >
{
    public OrderSpecification ( string Email ) : base ( O => O.UserEmail == Email )
    {
        AddInclude ( O => O.DeliveryMethod ) ;
        AddInclude ( O => O.Items ) ;
        AddOrderByDescending ( O => O.OrderDate ) ;
    }

    public OrderSpecification ( Guid id , string Email ) : base ( O
        => O.Id == id && ( string.IsNullOrEmpty ( Email ) || O.UserEmail.ToLower ( ) == Email.ToLower ( ) ) )
    {
        AddInclude ( O => O.DeliveryMethod ) ;
        AddInclude ( O => O.Items ) ;
    }
}