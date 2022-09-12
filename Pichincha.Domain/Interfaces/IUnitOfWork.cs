using Pichincha.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : Entity;
    }
}
