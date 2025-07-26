using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itasset.Application.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


        public string Position { get; set; }

        public string Email { get; set; }
    }
}
