using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public string Token1 { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
