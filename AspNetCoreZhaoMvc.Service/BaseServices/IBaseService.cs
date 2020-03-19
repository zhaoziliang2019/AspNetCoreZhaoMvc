using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreZhaoMvc.Service.BaseServices
{
    public interface IBaseService<T> where T :class
    {
        public IEnumerable<T> GetAlls();
        public T GetById(int Id);
        public bool Add(T t);
        public bool Update(T t);
        public bool Delete(int id);
    }
}
