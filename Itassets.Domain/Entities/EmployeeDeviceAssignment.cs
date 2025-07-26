
using System.ComponentModel.DataAnnotations.Schema;


namespace Itassets.Domain.Entities
{
    public class EmployeeDeviceAssignment
    {
        public Guid Id { get; set; }

        [ForeignKey("Device")]
        public Guid DeviceID { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeID { get; set; }    

        public DateOnly AssignDate { get; set; }

        public string DeviceStatus { get; set; }

        public DateOnly? ReturnDate {  get; set; }

        public string? ReturnStatus { get; set; }

        public Device Device { get; set; }

        public Employee Employee { get; set; }


    }
}
