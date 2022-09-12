using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Infrastructure.Repositories
{
    public class PersonaRepository : RepositoryBase<PersonaEntity>, IPersonaRepository
    {
        private readonly AppDbContext _context;
        public PersonaRepository(AppDbContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
    }
}
