using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreZhaoMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AspNetCoreZhaoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IMemoryCache memory;
        public HomeController(ILogger<HomeController> logger,IMemoryCache memory)
        {
            this.memory = memory;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            logger.LogInformation("","1331{0}",student.Id);
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Detail", new { id=student.FirstName});
            }
        }
    }
}