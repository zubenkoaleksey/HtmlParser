using System;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Link> Links { get; }
        IRepository<Css> Csses { get; }
        IRepository<Image> Images { get; }
        void Save();
    }
}
