
namespace Itassets.Domain.Entities
{
    public class DeviceTransfer
    {
        public Guid Id { get; set; }

        public Guid OldEmpId { get; set; }

        public Guid NewEmpId { get; set; }

        public Guid DeviceID { get; set; }

        public DateOnly DateOnly { get; set; }
    }
}
