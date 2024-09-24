using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public partial class Education
    {
        public int education_id { get; set; }
        public string? college_name { get; set; }
        public string? admission_year { get; set; }
        public string? passout_year { get; set; }
        public string? course { get; set; }
        public bool? is_deleted { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public int user_id { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
