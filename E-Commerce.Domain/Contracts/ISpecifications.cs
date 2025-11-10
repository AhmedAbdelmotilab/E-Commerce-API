using System.Linq.Expressions ;
using E_Commerce.Domain.Entities.SharedModule ;

namespace E_Commerce.Domain.Contracts ;

public interface ISpecifications < TEntity , TKey > where TEntity : BaseEntity < TKey >
{
    public ICollection < Expression < Func < TEntity , object > > > ? IncludeExpression { get ; }
    public Expression < Func < TEntity , bool > > ? Criteria { get ; }
    public Expression < Func < TEntity , object > > ? OrderBy { get ; }
    public Expression < Func < TEntity , object > > ? OrderByDescending { get ; }
}