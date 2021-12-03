using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, CTV")]
    [Route("Admin/Home/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
