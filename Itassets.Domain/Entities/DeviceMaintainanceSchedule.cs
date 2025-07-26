

using System.ComponentModel.DataAnnotations.Schema;

namespace Itassets.Domain.Entities
{
    public class DeviceMaintainanceSchedule
    {
        public Guid Id { get; set; }

        [ForeignKey("Device")]
        public Guid DeviceID { get; set; }

        public Device Device { get; set; }
        public string Description { get; set; }

        public DateOnly Date {  get; set; }

        public bool IsComplete {  get; set; } = false;

    }
}
