using Itassets.Domain.Entities;



namespace Itasset.Application.DTOs
{
    public class DeviceRequestsDTO
    {
        public Guid Id { get; set; }

       public Guid categoryID { get; set; }
        public string DeviceName { get; set; }

        public int DeviceCount { get; set; }

        public Guid officeId { get; set; }
        public DateOnly Date { get; set; }


        
    }
}
