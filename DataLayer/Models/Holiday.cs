using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Holiday
    {
        public int holiday_id { get; set; }
        public string? title { get; set; }
        //public DateOnly? holiday_date { get; set; }
        public DateTime? holiday_date { get; set; }  // Instead of DateOnly?
        public string? day { get; set; }
        public bool? is_deleted { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}
