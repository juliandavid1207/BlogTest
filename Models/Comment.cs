using System;
using System.Collections.Generic;

namespace BlogsWebApi.Models
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public string? Comment1 { get; set; }
        public int? IdPost { get; set; }
        public int? IdUser { get; set; }

        public virtual Post? IdPostNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
