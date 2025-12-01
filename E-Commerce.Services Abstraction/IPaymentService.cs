using E_Commerce.Shared.DTOs.BasketDTOs ;

namespace E_Commerce.Services_Abstraction ;

public interface IPaymentService
{
    Task < BasketDto > CreateOrUpdatePaymentAsync ( string basketId ) ;
    Task UpdatePaymentStatusAsync ( string request , string Header ) ;
}