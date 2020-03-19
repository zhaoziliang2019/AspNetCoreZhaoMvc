using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Repository.BaseRepositorys
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAlls();
        public Task<TEntity> GetById(object Id);
        public Task<bool> Add(TEntity model);
    }
}
