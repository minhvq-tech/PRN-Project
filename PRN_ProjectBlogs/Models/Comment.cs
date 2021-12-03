using System;
using System.Collections.Generic;

#nullable disable

namespace PRN_ProjectBlogs.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
