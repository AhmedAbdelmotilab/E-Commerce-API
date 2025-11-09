using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.SharedModule ;
using Microsoft.EntityFrameworkCore ;

namespace E_Commerce.Persistence.Evaluators ;

internal static class SpecificationEvaluator
{
    public static IQueryable < TEntity > CreateQuery < TEntity , TKey > (
        IQueryable < TEntity > entryPoint ,
        ISpecifications < TEntity , TKey > ? specifications ) where TEntity : BaseEntity < TKey >
    {
        var Query = entryPoint ;
        if ( specifications is not null && specifications.IncludeExpression.Any ( ) )
        {
            if ( specifications.IncludeExpression.Any ( ) )
            {
                Query = Query.Where ( specifications.Criteria ) ;
            }

            Query = specifications.IncludeExpression.Aggregate ( Query ,
                ( currentQuery , includeExp ) => currentQuery.Include ( includeExp ) ) ;
        }

        return Query ;
    }
}