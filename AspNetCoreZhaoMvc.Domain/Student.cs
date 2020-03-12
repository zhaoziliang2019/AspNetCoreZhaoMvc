using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspNetCoreZhaoMvc.Domain
{
    public class Student
    {
        [Display(Name ="姓")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "名")]
        public string LastName { get; set; }
    }
}
