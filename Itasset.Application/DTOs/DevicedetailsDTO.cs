using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class DevicedetailsDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public string Status { get; set; }
        public string Spex { get; set; }

        public int Warranty { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public bool IsFaulty { get; set; }
        public bool IsAvailable { get; set; }
    }
}
