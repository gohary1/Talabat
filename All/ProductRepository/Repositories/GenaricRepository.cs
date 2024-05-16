using Microsoft.EntityFrameworkCore;
using ProductData;
using ProductData.Entites;
using ProductData.Specifications;
using ProductRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Repositories
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDpContext _context;
        public GenaricRepository(StoreDpContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity)==typeof(Product))
            {
                var product= await _context.Set<Product>().Include(t=>t.Type).Include(b=>b.Brand).ToListAsync();
                return product.Cast<TEntity>().ToList();
            }
           return  await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsyncWithSpacification(ISpecification<TEntity,TKey> spec)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
           return await _context.Set<TEntity>().FindAsync(id);
        }



        public async Task<TEntity> GetByIdAsyncWithSpacification(ISpecification<TEntity, TKey> spec)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
