using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HtmlParser.DAL.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int Depth { get; set; }
        public TimeSpan LoadTime { get; set; }
        public decimal Size { get; set; }

        public int SiteId { get; set; }
        public Site Site { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Css> Csses { get; set; }

        public Link()
        {
            Images = new List<Image>();
            Csses = new List<Css>();
        }
    }
}
