using Hattrick.Model;

namespace Hattrick.Repository.Interfaces
{
    using System;
    using Hattrick.Model;

    public interface IUnitOfWork : IDisposable
    {
        ModelContext Context { get; }
    }
}