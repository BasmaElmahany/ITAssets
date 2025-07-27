using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class DeviceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Qty { get; set; }
        public string SerialNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public string Status { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryID { get; set; }
        public Guid? SupplierID { get; set; }
        public bool IsFaulty { get; set; }
        public bool IsAvailable { get; set; }
    }
}
