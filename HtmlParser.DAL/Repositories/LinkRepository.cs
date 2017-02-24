using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class LinkRepository : IRepository<Link>
    {
        private LinkContext _db;

        public LinkRepository(LinkContext context)
        {
            _db = context;
        }

        public IEnumerable<Link> GetAll()
        {
            return _db.Links;
        }

        public IEnumerable<Link> Find(Func<Link, bool> predicate)
        {
            return _db.Links.Where(predicate).ToList();
        }

        public Link Get(int id)
        {
            return _db.Links.Find(id);
        }

        public void Create(Link link)
        {
            _db.Links.Add(link);
        }

        public void Update(Link link)
        {
            _db.Entry(link).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Link link = _db.Links.Find(id);
            if (link != null)
            {
                _db.Links.Remove(link);
            }
        }
    }
}