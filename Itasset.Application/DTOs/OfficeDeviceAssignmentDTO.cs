
namespace Itasset.Application.DTOs
{
    public class OfficeDeviceAssignmentDTO
    {
        public Guid DeviceID { get; set; }
        public Guid OfficeID { get; set; }
        public DateOnly AssignDate { get; set; }
        public string DeviceStatus { get; set; }

        public int Qty { get; set; }
    }
}
