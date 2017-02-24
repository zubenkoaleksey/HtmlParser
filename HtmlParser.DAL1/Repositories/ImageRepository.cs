using System.Collections.Generic;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class ImageRepository : MyGenericRepository<Image>, IImageRepository
    {
        // add to db without links
        public void ImageStore(SortedSet<string> images)
        {
            // some logics for inspect dublicates
            StoreEntity(images, new Image());
        }
    }
}
