using System;
using System.Collections.Generic;

namespace BlogsWebApi.Models
{
    public partial class PostType
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        public int IdPostType { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
