﻿

namespace Itasset.Application.DTOs
{
    public class DeviceTransferDTO
    {
        public Guid OldEmpId { get; set; }

        public Guid NewEmpId { get; set; }

        public Guid DeviceID { get; set; }

        public DateOnly DateOnly { get; set; }
    }
}
