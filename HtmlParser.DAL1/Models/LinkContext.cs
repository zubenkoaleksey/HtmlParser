using System.Data.Entity;


namespace HtmlParser.DAL.Models
{
    public class LinkContext : DbContext
    {
        public LinkContext() : base("LinkConnection")
        { }

        static LinkContext()
        {
            Database.SetInitializer(new LinkInitializer());
        }

        public DbSet<Link> Links { get; set; }
        public DbSet<Css> Csses { get; set; }
        public DbSet<Image> Images { get; set; }

    }

    public class LinkInitializer : DropCreateDatabaseAlways<LinkContext>
    {
        protected override void Seed(LinkContext db)
        {
            base.Seed(db);
        }
    }
}
