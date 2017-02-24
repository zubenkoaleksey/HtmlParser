using System.Collections.Generic;

namespace HtmlParser.DAL.Models
{
    public class Css
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Link> Links { get; set; }

        public Css()
        {
            Links = new List<Link>();
        }
    }
}
