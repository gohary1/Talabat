using Microsoft.EntityFrameworkCore;
using ProductData.Entites;
using ProductData.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository
{
    public static class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity,TKey> spec)
        {
            var query = inputQuery;
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.orderBy is not null)
            {
                query=query.OrderBy(spec.orderBy);
            }
            else if (spec.orderByDesc is not null)
            {
                query = query.OrderByDescending(spec.orderBy);
            }
            if (spec.IsPaginationEnabled)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (current, includeExpresion) => current.Include(includeExpresion));
            return query;
        }
    }
}
