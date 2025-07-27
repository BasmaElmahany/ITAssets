

namespace Itasset.Application.DTOs
{
    public class EmployeeDeviceAssignmentDTO
    {
        public Guid DeviceID { get; set; }
        public Guid EmployeeID { get; set; }
        public DateOnly AssignDate { get; set; }
        public string DeviceStatus { get; set; }
        public int Qty { get; set; }
    }
}
