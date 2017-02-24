using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HtmlParser.DAL.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<Link> Links { get; set; }

        public Image()
        {
            Links = new List<Link>();
        }
    }
}
