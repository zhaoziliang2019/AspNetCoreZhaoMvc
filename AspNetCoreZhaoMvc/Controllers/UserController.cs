using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreZhaoMvc.Controllers
{
    [Authorize(Roles ="Adminstrators")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}