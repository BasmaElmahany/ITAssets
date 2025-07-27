
using System.ComponentModel.DataAnnotations.Schema;

namespace Itassets.Domain.Entities
{
    public class DeviceTransfer
    {
        public Guid Id { get; set; }

        [ForeignKey("emp1")]
        public Guid OldEmpId { get; set; }

        public Employee emp1 { get; set; }

        public Employee emp2 { get; set; }

        [ForeignKey("emp2")]
        public Guid NewEmpId { get; set; }

        [ForeignKey("device")]
        public Guid DeviceID { get; set; }


        public Device device { get; set; }

        public DateOnly DateOnly { get; set; }
    }
}
