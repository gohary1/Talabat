using ProductData.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenaricRepository<TEntity,int> Repository<TEntity>() where TEntity:BaseEntity<int>;
    }
}
