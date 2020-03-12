using AspNetCoreZhaoMvc.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreZhaoMvc.Repository.Students
{
    public class StudentRepository : IStudentRepository
    {
        public IEnumerable<Student> GetAlls()
        {
            return new List<Student>{
                new Student() {FirstName="12312"},
                new Student() { FirstName="132312"},
                new Student() {FirstName="13223123"},
                new Student() {FirstName="24354"},
            };
        }
    }
}
