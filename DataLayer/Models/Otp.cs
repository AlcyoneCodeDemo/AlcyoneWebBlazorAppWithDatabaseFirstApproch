using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Otp
    {
        public int OtpId { get; set; }
        public string? Otp1 { get; set; }
        public DateTime? OtpExpiresAt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserIdId { get; set; }

        public virtual User UserId { get; set; } = null!;
    }
}
