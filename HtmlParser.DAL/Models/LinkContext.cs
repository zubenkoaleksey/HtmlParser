using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser.DAL.Models
{
    public class LinkContext : DbContext
    {
        public LinkContext() : base("LinkConnection")
        { }

        public DbSet<Link> Links { get; set; }
        public DbSet<Css> Csses { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
