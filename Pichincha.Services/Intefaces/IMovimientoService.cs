using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Intefaces
{
    public interface IMovimientoService
    {
        Task<IEnumerable<MovimientoReadDto>> GetMovimientos();
        Task<MovimientoReadDto> GetMovimientoById(Guid id);
        Task<StatusDto> AddMovimiento(MovimientoCreateDto dto);
        Task UpdateMovimiento(Guid id, MovimientoDto dto);
        Task<StatusDto> RemoveMovimientoById(Guid id);
    }
}
