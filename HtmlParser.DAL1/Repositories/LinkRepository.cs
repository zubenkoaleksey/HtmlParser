using System;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class LinkRepository : MyGenericRepository<Link>, ILinkRepository
    {
        public void LinkStore(string urlToParse, string hostName, int currentDepth, TimeSpan ts, int size)
        {
            var match = Find(x => x.Url == urlToParse);
            if (match.Count() != 0) return;

            var link = new Link
            {
                Url = urlToParse,
                Depth = currentDepth,
                LoadTime = ts,
                Size = size,
                Site = new Site { HostName = hostName }
            };
            Create(link);
        }
    }
}
