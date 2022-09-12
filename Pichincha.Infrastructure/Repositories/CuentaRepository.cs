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
    public class CuentaRepository : Repository<CuentaEntity, Guid>, ICuentaRepository
    {
        private readonly AppDbContext _context;
        public CuentaRepository(AppDbContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }

        //public async Task<IEnumerable<CuentaReadDto>> SearchCuentas(CuentaSearchDto paramSeach)
        //{
        //    IEnumerable<CuentaReadDto> list = await (from Cuenta in _context.Cuenta
        //                                          where Cuenta.Name == paramSeach.Name || Cuenta.Breed == paramSeach.Breed || Cuenta.OwnerName == paramSeach.OwnerName || Cuenta.OwnerDni == paramSeach.OwnerDni
        //                                          select new CuentaReadDto
        //                                          {
        //                                              Name = Cuenta.Name,
        //                                              Breed = Cuenta.Breed,
        //                                              OwnerName = Cuenta.OwnerName,
        //                                              OwnerDni = Cuenta.OwnerDni,
        //                                              OwnerPhone = Cuenta.OwnerPhone,
        //                                              OwnerAddress = Cuenta.OwnerAddress
        //                                          }).ToListAsync();

        //    return list;
        //}
    }
}
