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
        if ( specifications is not null )
        {
            if ( specifications.Criteria is not null )
            {
                Query = Query.Where ( specifications.Criteria ) ;
            }

            if ( specifications.IncludeExpression is not null && specifications.IncludeExpression.Any ( ) )
            {
                Query = specifications.IncludeExpression.Aggregate ( Query ,
                    ( currentQuery , includeExp ) => currentQuery.Include ( includeExp ) ) ;
            }

            if ( specifications.OrderBy is not null )
            {
                Query = Query.OrderBy ( specifications.OrderBy ) ;
            }

            if ( specifications.OrderByDescending is not null )
            {
                Query = Query.OrderByDescending ( specifications.OrderByDescending ) ;
            }

            if ( specifications.IsPaginated == true )
            {
                Query = Query.Skip ( specifications.Skip ).Take ( specifications.Take ) ;
            }
        }

        return Query ;
    }
}