using Pichincha.Domain.Entities;
using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface ICuentaRepository : IRepository<CuentaEntity, Guid>
    {
    }
}
