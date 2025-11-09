using System.Linq.Expressions ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Services.Specifications ;

public abstract class BaseSpecification < TEntity , TKey > : ISpecifications < TEntity , TKey > where TEntity : BaseEntity < TKey >
{
    public BaseSpecification ( Expression < Func < TEntity , bool > > criteriaExpression )
    {
        Criteria = criteriaExpression ;
    }

    public Expression < Func < TEntity , bool > > Criteria { get ; }
    public ICollection < Expression < Func < TEntity , object > > > IncludeExpression { get ; } = [ ] ;

    protected void AddInclude ( Expression < Func < TEntity , object > > expression ) => IncludeExpression.Add ( expression ) ;
}