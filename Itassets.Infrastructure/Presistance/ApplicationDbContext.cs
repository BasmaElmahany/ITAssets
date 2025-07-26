using Itassets.Domain.Entities;
using Itassets.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Itassets.Infrastructure.Presistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        public DbSet<Brand> Brand {  get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceMaintainanceSchedule> DeviceMaintainanceSchedule { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeDeviceAssignment> EmployeeDeviceAssignment { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<OfficeDeviceAssignment> OfficeDeviceAssignment { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<DeviceRequests> DeviceRequests { get; set; }

        public DbSet<DeviceTransfer> DeviceTransfer { get; set; }
    }
}
