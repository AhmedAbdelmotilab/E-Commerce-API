using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.SharedModule ;
using E_Commerce.Persistence.Data.DbContexts ;

namespace E_Commerce.Persistence.Repositories ;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _dbContext ;
    private readonly Dictionary < Type , object > _repositories = [ ] ;

    public UnitOfWork ( StoreDbContext dbContext )
    {
        _dbContext = dbContext ;
    }

    public async Task < int > SavChangesAsync ( ) => await _dbContext.SaveChangesAsync ( ) ;

    public IGenericRepository < TEntity , TKey > GetRepository < TEntity , TKey > ( ) where TEntity : BaseEntity < TKey >
    {
        var EntityType = typeof ( TEntity ) ;
        if ( _repositories.TryGetValue ( EntityType , out object ? repository ) )
        {
            return ( IGenericRepository < TEntity , TKey > ) repository ;
        }

        var NewRepo = new GenericRepository < TEntity , TKey > ( _dbContext ) ;
        _repositories [ EntityType ] = NewRepo ;
        return NewRepo ;
    }
}