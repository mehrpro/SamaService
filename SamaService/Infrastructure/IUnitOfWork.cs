using System;
using System.Data.Entity;
using SamaService.Services;

namespace SamaService.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TagRepository TagRep { get; }
        void Commit();

    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly DbContext db;
        public UnitOfWork()
        {
            db = new TContext();
        }
        public void Commit()
        {
            db.SaveChanges();
        }

        private TagRepository _tagRep;
        public TagRepository TagRep => _tagRep ?? (_tagRep = new TagRepository(db));


        #region Disposed
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion

    }
}
