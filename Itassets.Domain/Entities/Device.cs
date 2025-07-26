
using System.ComponentModel.DataAnnotations.Schema;

namespace Itassets.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string? PhotoUrl { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public Brand Brand { get; set; }


        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Supplier")]
        public Guid? SupplierID { get; set; }

        public Supplier? Supplier { get; set; }
        public bool IsFaulty { get; set; }
        public bool IsAvailable { get; set; }


    }
}
