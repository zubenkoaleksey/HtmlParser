
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class SiteRepository : MyGenericRepository<Site>, ISiteRepository
    {
        // add host into db
        public void SiteStore(string hostName)
        {
            var site = new Site {HostName = hostName};
            Create(site);
        }
    }
}
