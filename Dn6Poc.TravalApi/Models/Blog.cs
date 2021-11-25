using System;
using System.Collections.Generic;

namespace Dn6Poc.TravalApi.Models
{
    public partial class Blog
    {
        public Blog()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Rating { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
