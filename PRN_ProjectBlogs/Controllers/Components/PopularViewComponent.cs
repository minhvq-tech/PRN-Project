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
    public class PopularViewComponent: ViewComponent
    {
        private readonly dbBlogsContext _context;
        private IMemoryCache _memoryCache;
        public PopularViewComponent(dbBlogsContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

        }
        public IViewComponentResult Invoke()
        {
            var _lsDanhMuc = _memoryCache.GetOrCreate(CacheKeys.Popular, entry =>
            {
                entry.SlidingExpiration = TimeSpan.MaxValue;
                return GetlsPosts();
            });
            return View(_lsDanhMuc);
        }
        public List<Post> GetlsPosts()
        {
            List<Post> lstins = new List<Post>();
            lstins = _context.Posts
                             .AsNoTracking()
                             .Where(x => x.Published == true)
                             .Take(6)
                             .ToList();
            return lstins;
        }
    }
}
