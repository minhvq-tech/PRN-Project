using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRN_ProjectBlogs.Enums;
using PRN_ProjectBlogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.Controllers.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly dbBlogsContext _context;
        private IMemoryCache _memoryCache;
        public CategoriesViewComponent(dbBlogsContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

        }
        public IViewComponentResult Invoke()
        {
            var _lsDanhMuc = _memoryCache.GetOrCreate(CacheKeys.Categories, entry =>
            {
                entry.SlidingExpiration = TimeSpan.MaxValue;
                return GetlsCategories();
            });
            return View(_lsDanhMuc);
        }
        public List<Category> GetlsCategories()
        {
            List<Category> lstins = new List<Category>();
            lstins = _context.Categories
                             .AsNoTracking()
                             .Where(x => x.Published == true)
                             .OrderBy(x => x.Ordering)
                             .ToList();
            return lstins;
        }
    }
}
