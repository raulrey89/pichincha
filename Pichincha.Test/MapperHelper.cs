using AutoMapper;
using Pichincha.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Test
{
    internal class MapperHelper
    {
        private static readonly object SyncObj = new object();
        private static bool _created;

        private static IMapper mapper;

        public static IMapper InitMappings()
        {
            lock (SyncObj)
                if (!_created)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<PichinchaMap>();
                    });

                    mapper = config.CreateMapper();
                    _created = true;
                }

            return mapper;
        }
    }
}
