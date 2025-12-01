using E_Commerce.Domain.Entities.OrderModule ;

namespace E_Commerce.Services.Specifications ;

public class OrderWithPaymentIntentSpecification : BaseSpecification < Order , Guid >
{
    public OrderWithPaymentIntentSpecification ( string paymentIndentId ) : base ( O => O.PaymentIntentId == paymentIndentId ) { }
}