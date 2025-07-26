using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Domain.Entities
{
    public class DeviceRequests
    {
        public Guid Id { get; set; }


        [ForeignKey("category")]
        public Guid categoryID { get; set; }

        public Category category { get; set; }

        public string DeviceName { get; set; }

        public int DeviceCount { get; set; }

        [ForeignKey("Office")]
        public Guid officeId { get; set; }

        public Office Office { get; set; } 


        public DateOnly Date { get; set; }

    }
}
