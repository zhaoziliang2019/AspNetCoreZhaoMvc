using AspNetCoreZhaoMvc.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Repository.BaseRepositorys
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DataContext dataContext;
        public async Task<bool> Add(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAlls()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(object Id)
        {
            throw new NotImplementedException();
        }
    }
}
