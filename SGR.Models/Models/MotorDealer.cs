using System;
using System.Collections.Generic;

#nullable disable

namespace SGR.Models.Models
{
    public partial class MotorDealer
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        public Guid DealerId { get; set; }
        public Guid MotorId { get; set; }

        public virtual Dealer Dealer { get; set; }
    }
}
