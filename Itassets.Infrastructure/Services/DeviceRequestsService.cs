using AutoMapper;
using Itassets.Infrastructure.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Infrastructure.Services
{
    public class DeviceRequestsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeviceRequestsService(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

    }
}
