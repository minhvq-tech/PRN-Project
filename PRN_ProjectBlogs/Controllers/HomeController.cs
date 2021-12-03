﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRN_ProjectBlogs.Models;
using PRN_ProjectBlogs.ModelViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbBlogsContext _context;
        public HomeController(ILogger<HomeController> logger, dbBlogsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           

            HomeViewModel model = new HomeViewModel();
            var ls = _context.Posts.Include(x => x.Cat).AsNoTracking().ToList();
            model.LastestPosts = ls;
            model.Populars = ls;
            model.Recents = ls;
            model.Trendings = ls;
            model.Inspirations = ls;
            model.Featured = ls.FirstOrDefault();
            return View(model);
        }
        [Route("lien-he.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
