using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //this is constrained to be used by only the entity classes
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec)
            {
                var query = inputQuery;

                //if there are some includes in the query to query the database
                if(spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria);  //p => p.ProductTypeId == Id
                }

                if (spec.OrderBy != null)
                {
                    query = query.OrderBy(spec.OrderBy);  
                }

                if (spec.OrderByDescending != null)
                {
                    query = query.OrderByDescending(spec.OrderByDescending); 
                }

                //paging should come after query and sorting.
                if (spec.IsPagingEnabled)
                {
                    query = query.Skip(spec.Skip).Take(spec.Take);
                }
            /* The next line is basically doing the following (just the include statements):
                return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
            */
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
                return query;
            }
    }
}