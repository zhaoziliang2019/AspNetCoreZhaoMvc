using AspNetCoreZhaoMvc.Service.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.ViewComponents
{
    public class WelcomeViewComponent:ViewComponent
    {
        //public readonly IStudentService studentService;
        //public WelcomeViewComponent(IStudentService _studentService)
        //{
        //    studentService = _studentService;
        //}

        public IViewComponentResult Invoke()
        {
            return View("Default","3");
        }
    }
}
