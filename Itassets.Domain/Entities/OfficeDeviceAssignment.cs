using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Domain.Entities
{
    public class OfficeDeviceAssignment
    {
        public Guid Id { get; set; }

        [ForeignKey("Device")]
        public Guid DeviceID { get; set; }

        [ForeignKey("Office")]
        public Guid OfficeID { get; set; }


        public DateOnly AssignDate { get; set; }

        public string DeviceStatus { get; set; }

        public DateOnly? ReturnDate { get; set; }

        public string? ReturnStatus { get; set; }


        public Device Device { get; set; }

        public Office Office { get; set; }
    }
}
