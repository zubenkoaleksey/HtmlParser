using System.Collections.Generic;

namespace HtmlParser.DAL.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Link> Links { get; set; }

        public Image()
        {
            Links = new List<Link>();
        }
    }
}
