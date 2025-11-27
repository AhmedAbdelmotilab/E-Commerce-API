using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Domain.Entities.OrderModule ;

public class Order : BaseEntity < Guid >
{
    public string UserEmail { get ; set ; } = default! ;
    public DateTimeOffset OrderDate { get ; set ; } = DateTimeOffset.Now ;
    public OrderStatus OrderStatus { get ; set ; } = OrderStatus.Pending ;
    public OrderAddress Address { get ; set ; } = default! ;
    public ICollection < OrderItem > Items { get ; set ; } = [ ] ;
    public decimal SubTotal { get ; set ; } // Total Price Of Items

    #region Method For Calculate The Total Price [(SubTotal) Total Price Of Items + (DeliveryMethod.Price) DeliveryMethodCost]

    public decimal GetTotal ( ) => SubTotal + DeliveryMethod.Price ;

    #endregion

    #region Relationships Order - Delivery Methods 1 | 1 <--> M

    public int DeliveryMethodId { get ; set ; }
    public DeliveryMethod DeliveryMethod { get ; set ; } = default! ;

    #endregion
}