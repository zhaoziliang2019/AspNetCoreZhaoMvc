using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreZhaoMvc.Repository.BaseRepositorys
{
    public interface IBaseRepository<T> where T:class
    {
        public IEnumerable<T> GetAlls();
    }
}
