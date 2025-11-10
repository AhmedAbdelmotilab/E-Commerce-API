using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Domain.Contracts ;

public interface IUnitOfWork
{
    Task < int > SavChangesAsync ( ) ;
    IGenericRepository < TEntity , TKey > GetRepository < TEntity , TKey > ( ) where TEntity : BaseEntity < TKey > ;
}