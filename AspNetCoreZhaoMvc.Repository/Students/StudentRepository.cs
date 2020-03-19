using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Repository.BaseRepositorys;
using AspNetCoreZhaoMvc.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Repository.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext context;

        public StudentRepository(DataContext _context)
        {
            context = _context;
        }

        public bool Add(Student t)
        {
            context.Students.Add(t);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<Student> GetAlls()
        {
            return context.Students.ToList();
        }

        public Student GetById(int Id)
        {
            return context.Students.Find(Id);
        }

        public Task<Student> GetById(object Id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseRepository<Student>.Add(Student model)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Student>> IBaseRepository<Student>.GetAlls()
        {
            throw new NotImplementedException();
        }
    }
}
