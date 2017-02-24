
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace HtmlParser.DAL.Models
{
    public class Site
    {
        [Key]
        public int Id { get; set; }
        public string HostName { get; set; }

        public ICollection<Link> Links { get; set; }

        public Site()
        {
            Links = new List<Link>();
        }
    }
}
