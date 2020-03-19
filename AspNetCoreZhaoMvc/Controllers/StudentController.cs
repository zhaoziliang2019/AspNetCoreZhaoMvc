using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreZhaoMvc.Service.Students;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreZhaoMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        public IActionResult Index()
        {
            var Students = studentService.GetAlls();
            ViewBag.SList = Students;
            return View();
        }
    }
}