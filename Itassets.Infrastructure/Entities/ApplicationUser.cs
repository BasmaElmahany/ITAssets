﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itassets.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string DesiredRole { get; set; }
    }
}
