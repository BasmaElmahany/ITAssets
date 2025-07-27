using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class OfficeDeviceReturnDTO
    {
        public Guid devAss {  get; set; }
        public DateOnly ReturnDate { get; set; }

        public string ReturnStatus { get; set; }
    }
}
