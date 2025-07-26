using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }


        public string Position { get; set; }

        public string Email { get; set; }

    }
}
