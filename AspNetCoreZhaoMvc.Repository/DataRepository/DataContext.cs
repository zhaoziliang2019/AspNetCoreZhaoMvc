using AspNetCoreZhaoMvc.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreZhaoMvc.Repository.DataRepository
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DbContext> options)
            :base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
