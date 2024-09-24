using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public partial class BankInformation
    {
        [Key]
        public int bank_id { get; set; }
        public string? bank_name { get; set; }
        public string? bank_accout_number { get; set; }
        public string? ifsc_code { get; set; }
        public string? pan_number { get; set; }
        public bool? is_deleted { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public int user_id { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
