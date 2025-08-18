using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class CreateDeviceWithPhotoRequest
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public IFormFile? Photo { get; set; }
        public string Status { get; set; }
        public string Spex { get; set; }

        public int Warranty { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryID { get; set; }
        public Guid? SupplierID { get; set; }
        public bool IsFaulty { get; set; }
        public bool IsAvailable { get; set; }

        public int Qty { get; set; }
    }
}
