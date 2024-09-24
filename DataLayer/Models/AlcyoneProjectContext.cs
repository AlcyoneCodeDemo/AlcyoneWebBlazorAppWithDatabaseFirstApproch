using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Models
{
    public partial class AlcyoneProjectContext : DbContext
    {
        public AlcyoneProjectContext()
        {
        }

        public AlcyoneProjectContext(DbContextOptions<AlcyoneProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthGroup> AuthGroups { get; set; } = null!;
        public virtual DbSet<AuthGroupPermission> AuthGroupPermissions { get; set; } = null!;
        public virtual DbSet<AuthPermission> AuthPermissions { get; set; } = null!;
        public virtual DbSet<AuthUser> AuthUsers { get; set; } = null!;
        public virtual DbSet<AuthUserGroup> AuthUserGroups { get; set; } = null!;
        public virtual DbSet<AuthUserUserPermission> AuthUserUserPermissions { get; set; } = null!;
        public virtual DbSet<BankInformation> BankInformations { get; set; } = null!;
        public virtual DbSet<DjangoAdminLog> DjangoAdminLogs { get; set; } = null!;
        public virtual DbSet<DjangoContentType> DjangoContentTypes { get; set; } = null!;
        public virtual DbSet<DjangoMigration> DjangoMigrations { get; set; } = null!;
        public virtual DbSet<DjangoSession> DjangoSessions { get; set; } = null!;
        public virtual DbSet<Education> Educations { get; set; } = null!;
        public virtual DbSet<ExperienceInformation> ExperienceInformations { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<Otp> Otps { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=AlcyoneProject;Username=postgres;Password=Year@2024");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthGroup>(entity =>
            {
                entity.ToTable("auth_group");

                entity.HasIndex(e => e.Name, "auth_group_name_a6ea08ec_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.Name, "auth_group_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<AuthGroupPermission>(entity =>
            {
                entity.ToTable("auth_group_permissions");

                entity.HasIndex(e => e.GroupId, "auth_group_permissions_group_id_b120cbf9");

                entity.HasIndex(e => new { e.GroupId, e.PermissionId }, "auth_group_permissions_group_id_permission_id_0cd325b0_uniq")
                    .IsUnique();

                entity.HasIndex(e => e.PermissionId, "auth_group_permissions_permission_id_84c5c92e");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_group_id_b120cbf9_fk_auth_group_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissio_permission_id_84c5c92e_fk_auth_perm");
            });

            modelBuilder.Entity<AuthPermission>(entity =>
            {
                entity.ToTable("auth_permission");

                entity.HasIndex(e => e.ContentTypeId, "auth_permission_content_type_id_2f476e4b");

                entity.HasIndex(e => new { e.ContentTypeId, e.Codename }, "auth_permission_content_type_id_codename_01ab375a_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codename)
                    .HasMaxLength(100)
                    .HasColumnName("codename");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.AuthPermissions)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_permission_content_type_id_2f476e4b_fk_django_co");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("auth_user");

                entity.HasIndex(e => e.Username, "auth_user_username_6821ab7c_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.Username, "auth_user_username_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateJoined).HasColumnName("date_joined");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsStaff).HasColumnName("is_staff");

                entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");

                entity.Property(e => e.LastLogin).HasColumnName("last_login");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<AuthUserGroup>(entity =>
            {
                entity.ToTable("auth_user_groups");

                entity.HasIndex(e => e.GroupId, "auth_user_groups_group_id_97559544");

                entity.HasIndex(e => e.UserId, "auth_user_groups_user_id_6a12ed8b");

                entity.HasIndex(e => new { e.UserId, e.GroupId }, "auth_user_groups_user_id_group_id_94350c0c_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_group_id_97559544_fk_auth_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_user_id_6a12ed8b_fk_auth_user_id");
            });

            modelBuilder.Entity<AuthUserUserPermission>(entity =>
            {
                entity.ToTable("auth_user_user_permissions");

                entity.HasIndex(e => e.PermissionId, "auth_user_user_permissions_permission_id_1fbb5f2c");

                entity.HasIndex(e => e.UserId, "auth_user_user_permissions_user_id_a95ead1b");

                entity.HasIndex(e => new { e.UserId, e.PermissionId }, "auth_user_user_permissions_user_id_permission_id_14a6b632_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permi_permission_id_1fbb5f2c_fk_auth_perm");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permissions_user_id_a95ead1b_fk_auth_user_id");
            });

            modelBuilder.Entity<BankInformation>(entity =>
            {
                entity.HasKey(e => e.bank_id)
                    .HasName("BankInformation_pkey");

                entity.ToTable("BankInformation");

                entity.HasIndex(e => e.user_id, "BankInformation_user_id_id_78f8d837");

                entity.Property(e => e.bank_id).HasColumnName("bank_id");

                entity.Property(e => e.bank_accout_number)
                    .HasMaxLength(30)
                    .HasColumnName("bank_accout_number");

                entity.Property(e => e.bank_name)
                    .HasMaxLength(100)
                    .HasColumnName("bank_name");

                entity.Property(e => e.created_by)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.created_on).HasColumnName("created_on");

                entity.Property(e => e.ifsc_code)
                    .HasMaxLength(20)
                    .HasColumnName("ifsc_code");

                entity.Property(e => e.is_deleted).HasColumnName("is_deleted");

                entity.Property(e => e.pan_number)
                    .HasMaxLength(30)
                    .HasColumnName("pan_number");

                entity.Property(e => e.updated_by)
                    .HasMaxLength(50)
                    .HasColumnName("updated_by");

                entity.Property(e => e.updated_on).HasColumnName("updated_on");

                entity.Property(e => e.user_id).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankInformations)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BankInformation_user_id_1d600370_fk_Users_user_id");
            });

            modelBuilder.Entity<DjangoAdminLog>(entity =>
            {
                entity.ToTable("django_admin_log");

                entity.HasIndex(e => e.ContentTypeId, "django_admin_log_content_type_id_c4bce8eb");

                entity.HasIndex(e => e.UserId, "django_admin_log_user_id_c564eba6");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionFlag).HasColumnName("action_flag");

                entity.Property(e => e.ActionTime).HasColumnName("action_time");

                entity.Property(e => e.ChangeMessage).HasColumnName("change_message");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ObjectRepr)
                    .HasMaxLength(200)
                    .HasColumnName("object_repr");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.DjangoAdminLogs)
                    .HasForeignKey(d => d.ContentTypeId)
                    .HasConstraintName("django_admin_log_content_type_id_c4bce8eb_fk_django_co");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DjangoAdminLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("django_admin_log_user_id_c564eba6_fk_auth_user_id");
            });

            modelBuilder.Entity<DjangoContentType>(entity =>
            {
                entity.ToTable("django_content_type");

                entity.HasIndex(e => new { e.AppLabel, e.Model }, "django_content_type_app_label_model_76bd3d3b_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppLabel)
                    .HasMaxLength(100)
                    .HasColumnName("app_label");

                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .HasColumnName("model");
            });

            modelBuilder.Entity<DjangoMigration>(entity =>
            {
                entity.ToTable("django_migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.App)
                    .HasMaxLength(255)
                    .HasColumnName("app");

                entity.Property(e => e.Applied).HasColumnName("applied");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DjangoSession>(entity =>
            {
                entity.HasKey(e => e.SessionKey)
                    .HasName("django_session_pkey");

                entity.ToTable("django_session");

                entity.HasIndex(e => e.ExpireDate, "django_session_expire_date_a5c62663");

                entity.HasIndex(e => e.SessionKey, "django_session_session_key_c0390e0f_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.Property(e => e.SessionKey)
                    .HasMaxLength(40)
                    .HasColumnName("session_key");

                entity.Property(e => e.ExpireDate).HasColumnName("expire_date");

                entity.Property(e => e.SessionData).HasColumnName("session_data");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Education");

                entity.HasIndex(e => e.user_id, "Education_user_id_id_0130a561");

                entity.Property(e => e.education_id).HasColumnName("education_id");

                entity.Property(e => e.admission_year)
                    .HasMaxLength(4)
                    .HasColumnName("admission_year");

                entity.Property(e => e.college_name)
                    .HasMaxLength(100)
                    .HasColumnName("college_name");

                entity.Property(e => e.course)
                    .HasMaxLength(30)
                    .HasColumnName("course");

                entity.Property(e => e.created_by)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.created_on).HasColumnName("created_on");

                entity.Property(e => e.is_deleted).HasColumnName("is_deleted");

                entity.Property(e => e.passout_year)
                    .HasMaxLength(4)
                    .HasColumnName("passout_year");

                entity.Property(e => e.updated_by)
                    .HasMaxLength(50)
                    .HasColumnName("updated_by");

                entity.Property(e => e.updated_on).HasColumnName("updated_on");

                entity.Property(e => e.user_id).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Education_user_id_1a2a8bd6_fk_Users_user_id");
            });

            modelBuilder.Entity<ExperienceInformation>(entity =>
            {
                entity.HasKey(e => e.ExperienceId)
                    .HasName("ExperienceInformation_pkey");

                entity.ToTable("ExperienceInformation");

                entity.HasIndex(e => e.UserId, "ExperienceInformation_user_id_id_903a04ed");

                entity.Property(e => e.ExperienceId).HasColumnName("experience_id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.DateOfJoining)
                    .HasColumnType("character varying")
                    .HasColumnName("date_of_joining");

                entity.Property(e => e.DateOfRelieving)
                    .HasColumnType("character varying")
                    .HasColumnName("date_of_relieving");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Position)
                    .HasMaxLength(100)
                    .HasColumnName("position");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExperienceInformations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExperienceInformation_user_id_9daa193b_fk_Users_user_id");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holiday");

                entity.Property(e => e.holiday_id).HasColumnName("holiday_id");

                entity.Property(e => e.created_by)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.created_on).HasColumnName("created_on");

                entity.Property(e => e.day)
                    .HasMaxLength(20)
                    .HasColumnName("day");

                entity.Property(e => e.holiday_date).HasColumnName("holiday_date");

                entity.Property(e => e.is_deleted).HasColumnName("is_deleted");

                entity.Property(e => e.title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.updated_by)
                    .HasMaxLength(50)
                    .HasColumnName("updated_by");

                entity.Property(e => e.updated_on).HasColumnName("updated_on");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("OTP");

                entity.HasIndex(e => e.UserIdId, "OTP_user_id_id_35b0de0e");

                entity.Property(e => e.OtpId).HasColumnName("otp_id");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.Otp1)
                    .HasMaxLength(6)
                    .HasColumnName("otp");

                entity.Property(e => e.OtpExpiresAt).HasColumnName("otp_expires_at");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.UserIdId).HasColumnName("user_id_id");

                entity.HasOne(d => d.UserId)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.UserIdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OTP_user_id_id_35b0de0e_fk_Users_user_id");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.HasIndex(e => e.Token1, "Token_token_1a37d83a_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.Token1, "Token_token_key")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "Token_user_id_5b7f767e");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

                entity.Property(e => e.Token1)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.Property(e => e.Used).HasColumnName("used");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Token_user_id_5b7f767e_fk_Users_user_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.email, "Users_email_93eda431_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.email, "Users_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.emp_id, "Users_emp_id_5b90f49d_like")
                    .HasOperators(new[] { "varchar_pattern_ops" });

                entity.HasIndex(e => e.emp_id, "Users_emp_id_key")
                    .IsUnique();

                entity.Property(e => e.user_id).HasColumnName("user_id");

                entity.Property(e => e.address).HasColumnName("address");

                entity.Property(e => e.blood_group)
                    .HasMaxLength(10)
                    .HasColumnName("blood_group");

                entity.Property(e => e.contact)
                    .HasMaxLength(20)
                    .HasColumnName("contact");

                entity.Property(e => e.country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.created_by)
                    .HasMaxLength(50)
                    .HasColumnName("created_by");

                entity.Property(e => e.created_on).HasColumnName("created_on");

                entity.Property(e => e.email)
                    .HasMaxLength(254)
                    .HasColumnName("email");

                entity.Property(e => e.emergency_contact)
                    .HasMaxLength(20)
                    .HasColumnName("emergency_contact");

                entity.Property(e => e.emergency_contact_details)
                    .HasColumnType("jsonb")
                    .HasColumnName("emergency_contact_details");

                entity.Property(e => e.emp_id)
                    .HasMaxLength(50)
                    .HasColumnName("emp_id");

                entity.Property(e => e.is_deleted).HasColumnName("is_deleted");

                entity.Property(e => e.is_google_register).HasColumnName("is_google_register");

                entity.Property(e => e.is_linkedin_register).HasColumnName("is_linkedin_register");

                entity.Property(e => e.is_verified).HasColumnName("is_verified");

                entity.Property(e => e.marital_status)
                    .HasMaxLength(20)
                    .HasColumnName("marital_status");

                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.nationality)
                    .HasMaxLength(50)
                    .HasColumnName("nationality");

                entity.Property(e => e.password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.profile_photo)
                    .HasMaxLength(100)
                    .HasColumnName("profile_photo");

                entity.Property(e => e.religion)
                    .HasMaxLength(50)
                    .HasColumnName("religion");

                entity.Property(e => e.role)
                    .HasMaxLength(100)
                    .HasColumnName("role");

                entity.Property(e => e.state)
                    .HasMaxLength(50)
                    .HasColumnName("state");

                entity.Property(e => e.status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.terms_and_conditions).HasColumnName("terms_and_conditions");

                entity.Property(e => e.updated_by)
                    .HasMaxLength(50)
                    .HasColumnName("updated_by");

                entity.Property(e => e.updated_on).HasColumnName("updated_on");

                entity.Property(e => e.zipcode)
                    .HasMaxLength(20)
                    .HasColumnName("zipcode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
