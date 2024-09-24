using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{
    public class EducationData
    {
        public int EducationId { get; set; }
        public string? CollegeName { get; set; }
        public string? AdmissionYear { get; set; }
        public string? PassoutYear { get; set; }
        public string? Course { get; set; }
        //public bool? IsDeleted { get; set; }
        //public string? CreatedBy { get; set; }
        //public string? UpdatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public int UserId { get; set; }
    }
}
