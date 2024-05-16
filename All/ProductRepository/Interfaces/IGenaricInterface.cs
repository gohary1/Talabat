using ProductData.Entites;
using ProductData.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Interfaces
{

        public interface IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
        {
            Task<List<TEntity>> GetAllAsync();
            Task<List<TEntity>> GetAllAsyncWithSpacification(ISpecification<TEntity,TKey> spec);
            Task<TEntity> GetByIdAsync(TKey id);
            Task<TEntity> GetByIdAsyncWithSpacification(ISpecification<TEntity, TKey> spec);
            Task AddAsync(TEntity entity);
            void Update(TEntity entity);
            void Delete(TEntity entity);
        }
}
