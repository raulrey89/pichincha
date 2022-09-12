using Pichincha.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface IPersonaRepository : IRepository<PersonaEntity, Guid>
    {
    }
}
