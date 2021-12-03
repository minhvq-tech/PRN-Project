using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PRN_ProjectBlogs.Enums;
using PRN_ProjectBlogs.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.Controllers.Components
{
    public class SocialViewComponent : ViewComponent
    {
        private readonly IConfiguration _config;
        private IMemoryCache _memoryCache;
        public SocialViewComponent(IMemoryCache memoryCache, IConfiguration config)
        {
            _memoryCache = memoryCache;
            _config = config;
            

        }
        public IViewComponentResult Invoke()
        {
            var _social = _memoryCache.GetOrCreate(CacheKeys.Social, entry =>
            {
                entry.SlidingExpiration = TimeSpan.MaxValue;
                return GetlsSocial();
            });
            return View(_social);
        }
        public SocialVM GetlsSocial()
        {
            SocialVM socialVM = new SocialVM();

            socialVM.Facebook = _config.GetValue<string>("SocialLinks:facebook");
            socialVM.Twitter = _config.GetValue<string>("SocialLinks:twitter");
            socialVM.Youtube = _config.GetValue<string>("SocialLinks:youtube");
            socialVM.Instagram = _config.GetValue<string>("SocialLinks:instagram");
            return socialVM;
        }
    }
}
