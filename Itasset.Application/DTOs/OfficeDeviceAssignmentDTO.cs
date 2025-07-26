using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class OfficeDeviceAssignmentDTO
    {
        public Guid Id { get; set; }
        public Guid DeviceID { get; set; }
        public Guid OfficeID { get; set; }
        public DateOnly AssignDate { get; set; }
        public string DeviceStatus { get; set; }
    }
}
