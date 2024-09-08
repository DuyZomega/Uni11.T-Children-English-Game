using CEG_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepositories AccountRepositories { get; }
        void Save();
    }
}
