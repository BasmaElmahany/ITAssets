using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class EmployeeDeviceAssignmentDTO
    {
        public Guid Id { get; set; }
        public Guid DeviceID { get; set; }
        public Guid EmployeeID { get; set; }
        public DateOnly AssignDate { get; set; }
        public string DeviceStatus { get; set; }
    }
}
