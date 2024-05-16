using ProductData.Entites;
using ProductRepository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDpContext _context;
        private  Hashtable _repo;

        public UnitOfWork(StoreDpContext context)
        {
            _context = context;
            _repo = new Hashtable();
        }
        public async Task<int> CompleteAsync()
           =>await _context.SaveChangesAsync();


        public async ValueTask DisposeAsync()
        =>await _context.DisposeAsync();
     

        public IGenaricRepository<TEntity, int> Repository<TEntity>() where TEntity : BaseEntity<int>
        {
            var type = typeof(TEntity).Name;
            if (!_repo.ContainsKey(type))
            {
                var repository = new GenaricRepository<TEntity, int>(_context);
                _repo.Add(type, repository);
            }
            return (GenaricRepository<TEntity,int>) _repo[type];
        }
    }
}
