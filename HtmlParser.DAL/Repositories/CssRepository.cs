using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class CssRepository : IRepository<Css>
    {
        private LinkContext _db;

        public CssRepository(LinkContext context)
        {
            _db = context;
        }

        public IEnumerable<Css> GetAll()
        {
            return _db.Csses;
        }

        public Css Get(int id)
        {
            return _db.Csses.Find(id);
        }

        public IEnumerable<Css> Find(Func<Css, bool> predicate)
        {
            return _db.Csses.Where(predicate).ToList();
        }

        public void Create(Css css)
        {
            _db.Csses.Add(css);
        }

        public void Update(Css css)
        {
            _db.Entry(css).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Css css = _db.Csses.Find(id);
            if (css != null)
            {
                _db.Csses.Remove(css);
            }
        }
    }
}
