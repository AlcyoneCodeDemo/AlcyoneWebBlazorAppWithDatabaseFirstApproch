using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            BankInformations = new HashSet<BankInformation>();
            Educations = new HashSet<Education>();
            ExperienceInformations = new HashSet<ExperienceInformation>();
            Otps = new HashSet<Otp>();
            Tokens = new HashSet<Token>();
        }

        public int user_id { get; set; }
        public string emp_id { get; set; } = null!;
        public string? name { get; set; }
        public string email { get; set; } = null!;
        public string? status { get; set; }
        public string? role { get; set; }
        public string? contact { get; set; }
        public string? emergency_contact { get; set; }
        public string? blood_group { get; set; }
        public string? nationality { get; set; }
        public string? religion { get; set; }
        public string? marital_status { get; set; }
        public string? address { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? zipcode { get; set; }
        public string? emergency_contact_details { get; set; }
        public bool? is_deleted { get; set; }
        public bool? is_verified { get; set; }
        public bool? is_google_register { get; set; }
        public bool? is_linkedin_register { get; set; }
        public bool? terms_and_conditions { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public string? password { get; set; }
        public string? profile_photo { get; set; }

        public virtual ICollection<BankInformation> BankInformations { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<ExperienceInformation> ExperienceInformations { get; set; }
        public virtual ICollection<Otp> Otps { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
