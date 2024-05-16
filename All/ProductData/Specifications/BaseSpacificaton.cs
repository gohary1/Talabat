using ProductData.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Specifications
{
    public class BaseSpacificaton<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> orderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> orderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpacificaton(Expression<Func<TEntity,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;  
        }
        public BaseSpacificaton()
        {

        }
        
        public void AddOrderBy(Expression<Func<TEntity, object>> order)
        {
            orderBy=order;
        } 
        public void AddOrderByDesc(Expression<Func<TEntity, object>> order)
        {
            orderByDesc=order;
        }
        public void applyPagination(int Takee,int Skipp)
        {
            Skip = Skipp;
            Take = Takee;
            IsPaginationEnabled = true;
        }
    }
}
