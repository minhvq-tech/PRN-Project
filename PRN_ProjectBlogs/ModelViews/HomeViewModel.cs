using PRN_ProjectBlogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.ModelViews
{
    public class HomeViewModel
    {
        public List<Post> Populars { get; set; }
        public List<Post> Inspirations { get; set; }
        public List<Post> Recents { get; set; }
        public List<Post> Trendings { get; set; }
        public List<Post> LastestPosts { get; set; }
        public Post Featured { get; set; }


    }
}
