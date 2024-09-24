using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class ExperienceInformation
    {
        [Key]
        public int ExperienceId { get; set; }
        public string? Position { get; set; }
        public string? DateOfJoining { get; set; }
        public string? DateOfRelieving { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
