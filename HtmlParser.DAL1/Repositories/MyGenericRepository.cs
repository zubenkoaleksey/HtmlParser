using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public abstract class MyGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected LinkContext Context;
        protected DbSet<TEntity> DbSet;

        protected MyGenericRepository()
        {
            Context = new LinkContext();
            DbSet = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            TEntity entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
            Context.SaveChanges();
        }

        public void StoreEntity(SortedSet<string> collection, TEntity entity)
        {
            var type = entity.GetType();

            if (type == typeof(Image))
            {
                foreach (var item in collection)
                {
                    var imgItem = new Image { Url = item };
                    Create(imgItem as TEntity);
                }
            }
            if (type == typeof(Css))
            {
                foreach (var item in collection)
                {
                    var cssItem = new Css { Url = item };
                    Create(cssItem as TEntity);
                }
            }


        }
    }
}
