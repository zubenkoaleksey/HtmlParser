using System;
using System.Collections.Generic;

namespace HtmlParser.DAL.Interfaces
{
    public interface IRepository<TEntity> 
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
    }
}
