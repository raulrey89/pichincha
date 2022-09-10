using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitChanges();
        void RollbackChanges();
    }
}
