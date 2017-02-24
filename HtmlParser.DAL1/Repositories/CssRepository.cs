
using System.Collections.Generic;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class CssRepository : MyGenericRepository<Css>, ICssRepository
    {
        // add css to db without links
        public void CssStore(SortedSet<string> css)
        {
            // some logics for inspect dublicates
            StoreEntity(css, new Css());
        }
    }
}
