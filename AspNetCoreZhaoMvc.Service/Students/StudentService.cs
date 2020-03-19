using AspNetCoreZhaoMvc.Domain;
using AspNetCoreZhaoMvc.Repository.Students;
using AspNetCoreZhaoMvc.Service.BaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Service.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }
        public Task<bool> Add(Student t)
        {
            return studentRepository.Add(t);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAlls()
        {
            return studentRepository.GetAlls();
        }

        public Task<Student> GetById(int Id)
        {
            return studentRepository.GetById(Id);
        }

        public bool Update(Student t)
        {
            throw new NotImplementedException();
        }

        bool IBaseService<Student>.Add(Student t)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Student> IBaseService<Student>.GetAlls()
        {
            throw new NotImplementedException();
        }

        Student IBaseService<Student>.GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
