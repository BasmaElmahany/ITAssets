using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class DeviceMaintainanceScheduleDTO
    {
        public Guid DeviceID { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public bool IsComplete { get; set; } = false;

    }
}
