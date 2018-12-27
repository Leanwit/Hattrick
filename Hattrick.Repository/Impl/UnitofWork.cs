namespace Hattrick.Repository.Impl
{
    using System;
    using Hattrick.Model;
    using Hattrick.Repository.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        public ModelContext Context { get; private set; }

        public UnitOfWork(ModelContext modelContext)
        {
            this.Context = modelContext;
        }

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }

}