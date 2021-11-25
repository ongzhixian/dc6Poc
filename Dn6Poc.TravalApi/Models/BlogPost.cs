using System;
using System.Collections.Generic;

namespace Dn6Poc.TravalApi.Models
{
    public partial class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string MarkupContent { get; set; } = null!;
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; } = null!;
    }
}
