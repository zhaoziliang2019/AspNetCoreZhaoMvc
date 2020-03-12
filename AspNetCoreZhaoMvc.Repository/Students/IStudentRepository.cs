using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Repository.BaseRepositorys;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreZhaoMvc.Repository.Students
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
        
    }
}
