using AutoMapper;
using Pichincha.Domain.Entities;
using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Profiles
{
    public class PichinchaMap : Profile
    {
        public PichinchaMap()
        {

            CreateMap<MovimientoEntity, MovimientoCreateDto>().ReverseMap();

        }
    }
}
