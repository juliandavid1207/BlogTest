using System;
using System.Collections.Generic;

namespace BlogsWebApi.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int IdPost { get; set; }
        public int? PostType { get; set; }
        public string? PostPath { get; set; }
        public int? IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
        public virtual PostType? PostTypeNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
