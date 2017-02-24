using System;
using HtmlParser.DAL.Interfaces;
using HtmlParser.DAL.Models;

namespace HtmlParser.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private LinkContext _db;
        private LinkRepository _linkRepository;
        private ImageRepository _imageRepository;
        private CssRepository _cssRepository;

        public UnitOfWork(LinkContext context)
        {
            _db = context;
        }

        public IRepository<Link> Links
        {
            get
            {
                if (_linkRepository == null)
                    _linkRepository = new LinkRepository(_db);
                return _linkRepository;
            }
        }

        public IRepository<Image> Images
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_db);
                return _imageRepository;
            }
        }

        public IRepository<Css> Csses
        {
            get
            {
                if (_cssRepository == null)
                    _cssRepository = new CssRepository(_db);
                return _cssRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
