using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private LinkContext _db;

        public ImageRepository(LinkContext context)
        {
            _db = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return _db.Images;
        }

        public IEnumerable<Image> Find(Func<Image, bool> predicate)
        {
            return _db.Images.Where(predicate).ToList();
        }

        public Image Get(int id)
        {
            return _db.Images.Find(id);
        }

        public void Create(Image image)
        {
            _db.Images.Add(image);
        }

        public void Update(Image image)
        {
            _db.Entry(image).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Image image = _db.Images.Find(id);
            if (image != null)
            {
                _db.Images.Remove(image);
            }
        }
    }
}
