using Entities.Models;
using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CollegeRefMajor> CollegeRefMajor { get; set; }
        public virtual DbSet<Colleges> Colleges { get; set; }
        public virtual DbSet<LessionDetails> LessionDetails { get; set; }
        public virtual DbSet<RecommentLession> Lessions { get; set; }
        public virtual DbSet<MajorRefPersonality> MajorRefPersonality { get; set; }
        public virtual DbSet<Majors> Majors { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SecurityCode> SecurityCode { get; set; }
        public virtual DbSet<SysUserRole> SysUserRole { get; set; }
        public virtual DbSet<TestAnswers> TestAnswers { get; set; }
        public virtual DbSet<AnswersPGroups> AnswersPerGroups { get; set; }
        public virtual DbSet<TestDeclarations> TestDeclarations { get; set; }
        public virtual DbSet<TestPersonalityGroups> TestPersonalityGroups { get; set; }
        public virtual DbSet<TestQuestions> TestQuestions { get; set; }
        public virtual DbSet<TestResults> TestResults { get; set; }
        public virtual DbSet<TestTypes> TestTypes { get; set; }
        public virtual DbSet<UserLession> UserLession { get; set; }
        public virtual DbSet<VcGuidance> VcGuidance { get; set; }

        public virtual DbSet<MajorSubjectGroup> MajorSubjectGroup { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroup { get; set; }
        public virtual DbSet<SubjectGroupSubject> SubjectGroupSubject { get; set; }
        public virtual DbSet<UserSubject> UserSubject { get; set; }
        public virtual DbSet<UserSubjectGroup> UserSubjectGroups { get; set; }
        public virtual DbSet<UserMajor> UserMajors { get; set; }
        public virtual DbSet<CollegesSubjectGroup> CollegesSubjectGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=capssummer.cmmwek4pr8zn.us-east-1.rds.amazonaws.com;Database=MajorTest;user id=admin;password=Summer2022;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MajorSubjectGroup>(entity =>
            {
                entity.HasKey(e => new { e.MajorId, e.SubjectGroupId });

                entity.ToTable("Major_SubjectGroup");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.SubjectGroups)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMajor_SubjectGroup_Major");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Majors)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMajor_SubjectGroup_SubjectGroup");
            });

            modelBuilder.Entity<UserMajor>(entity =>
            {
                entity.HasKey(e => new { e.MajorId, e.UserId });

                entity.ToTable("User_Major");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser_Major_Major");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Majors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser_Major_User");
            });

            modelBuilder.Entity<CollegesSubjectGroup>(entity =>
            {
                entity.HasKey(e => new { e.CollegesId, e.SubjectGroupId });

                entity.ToTable("Colleges_SubjectGroup");

                entity.HasOne(d => d.College)
                    .WithMany(p => p.SubjectGroups)
                    .HasForeignKey(d => d.CollegesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKColleges_SubjectGroup_Colleges");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Colleges)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKColleges_SubjectGroup_SubjectGroup");
            });

            modelBuilder.Entity<SubjectGroupSubject>(entity =>
            {
                entity.HasKey(e => new { e.GroupSubjectId, e.SubjectId });

                entity.ToTable("Subject_SubjectGroup");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.GroupSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubject_SubjectGroup_SubjectGroup");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectGroups)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubject_SubjectGroup_Subject");
            });

            modelBuilder.Entity<UserSubject>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectId });

                entity.ToTable("Subject_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubject_User_User");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubject_User_Subject");
            });

            modelBuilder.Entity<UserSubjectGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectGroupId });

                entity.ToTable("SubjectGroup_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubjectGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubjectGroup_User_User");

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSubjectGroup_User_Subject");
            });

            modelBuilder.Entity<CollegeRefMajor>(entity =>
            {
                entity.HasKey(e => new { e.MajorId, e.CollegeId })
                    .HasName("PK__College___572CB6E089C2CACB");

                entity.ToTable("College_Ref_Major");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.CollegeId).HasColumnName("CollegeID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.HasOne(d => d.College)
                    .WithMany(p => p.CollegeRefMajor)
                    .HasForeignKey(d => d.CollegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCollege_Re287083");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.CollegeRefMajor)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCollege_Re494710");
            });

            modelBuilder.Entity<Colleges>(entity =>
            {
                entity.HasKey(e => e.CollegeId)
                    .HasName("PK__Colleges__29409519E55EE24B");

                entity.Property(e => e.CollegeId).HasColumnName("CollegeID");

                entity.Property(e => e.CollegeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

            });

            modelBuilder.Entity<LessionDetails>(entity =>
            {
                entity.HasKey(e => e.LessionDetailId)
                    .HasName("PK__Learning__40D9D999F16AA74B");

                entity.Property(e => e.LessionDetailId).HasColumnName("LessionDetailID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.LessionDetailContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LessionId).HasColumnName("LessionID");

                entity.Property(e => e.OrderIndex).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Lession)
                    .WithMany(p => p.LessionDetails)
                    .HasForeignKey(d => d.LessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLearningPa175463");
            });

            modelBuilder.Entity<RecommentLession>(entity =>
            {
                entity.HasKey(e => e.LessionId)
                    .HasName("PK__Learning__20DCAEA1FA5B114D");

                entity.ToTable("Learning_Paths");

                entity.Property(e => e.LessionId).HasColumnName("LessionID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Lessions)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLearning_P92098");
            });

            modelBuilder.Entity<MajorRefPersonality>(entity =>
            {
                entity.HasKey(e => new { e.PersonalityGroupId, e.MajorId })
                    .HasName("PK__Major_Re__E28E6459D09867C3");

                entity.ToTable("Major_Ref_Personality");

                entity.Property(e => e.PersonalityGroupId).HasColumnName("PersonalityGroupID");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.MajorRefPersonality)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMajor_Ref_224122");

                entity.HasOne(d => d.PersonalityGroup)
                    .WithMany(p => p.MajorRefPersonality)
                    .HasForeignKey(d => d.PersonalityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMajor_Ref_93769");
            });

            modelBuilder.Entity<Majors>(entity =>
            {
                entity.HasKey(e => e.MajorId)
                    .HasName("PK__Majors__D5B8BFB1FFCDC09F");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.MajorName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_SYSTEM_USER");

                entity.ToTable("Sys_User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Gender)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsLocked)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.Property(e => e.UserName).HasMaxLength(60);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SysUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSys_User142060");
            });

            modelBuilder.Entity<SecurityCode>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("SECURITY_CODE");
            });

            modelBuilder.Entity<SysUserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_USER_POSITION");

                entity.ToTable("Sys_User_Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TestAnswers>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK__Test_Ans__D4825024C8BB5FCE");

                entity.ToTable("Test_Answers");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerContent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Answe601746");
            });

            modelBuilder.Entity<AnswersPGroups>(entity =>
            {
                entity.HasOne(d => d.Answer)
                .WithMany(p => p.AnswerPGroups)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FKAnswer_PGroup");

                entity.HasOne(d => d.PGroup)
                .WithMany(p => p.PGroupAnswers)
                .HasForeignKey(d => d.PGroupId)
                .HasConstraintName("FKPGroup_Answer");
            });

            modelBuilder.Entity<TestDeclarations>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__Test_Dec__8CC33100AFED4550");

                entity.ToTable("Test_Declarations");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TestDescrip).HasMaxLength(50);

                entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");

                entity.HasOne(d => d.TestType)
                    .WithMany(p => p.TestDeclarations)
                    .HasForeignKey(d => d.TestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Decla953794");
            });

            modelBuilder.Entity<TestPersonalityGroups>(entity =>
            {
                entity.HasKey(e => e.PersonalityGroupId)
                    .HasName("PK__Test_Per__EFD5EFA226ABA06D");

                entity.ToTable("Test_Personality_Groups");

                entity.Property(e => e.PersonalityGroupId).HasColumnName("PersonalityGroupID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.PersonalityGroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");

                entity.HasOne(d => d.TestType)
                    .WithMany(p => p.TestPersonalityGroups)
                    .HasForeignKey(d => d.TestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Perso404711");
            });

            modelBuilder.Entity<TestQuestions>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Test_Que__0DC06F8C223898C0");

                entity.ToTable("Test_Questions");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.QuestionContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestQuestions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Quest411060");
            });

            modelBuilder.Entity<TestResults>(entity =>
            {
                entity.HasKey(e => e.TestResultId)
                    .HasName("PK__Test_Res__E2463A674228CD84");

                entity.ToTable("Test_Results");

                entity.Property(e => e.TestResultId).HasColumnName("TestResultID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsLast)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Resul169939");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Resul345735");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Resul403288");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTest_Resul11694");
            });

            modelBuilder.Entity<TestTypes>(entity =>
            {
                entity.HasKey(e => e.TestTypeId)
                    .HasName("PK__Test_Typ__9BB876465EDCF709");

                entity.ToTable("Test_Types");

                entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TestTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserLession>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LessionId })
                    .HasName("PK__User_Lea__15850646891BC882");

                entity.ToTable("User_Lession");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.LessionId).HasColumnName("LessionID");

                entity.HasOne(d => d.Lession)
                    .WithMany(p => p.UserLession)
                    .HasForeignKey(d => d.LessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser_Learn182352");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLession)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser_Learn707353");
            });

            modelBuilder.Entity<VcGuidance>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CollegeId, e.MajorId })
                    .HasName("PK__VC_Guida__25C97D42CD3C4906");

                entity.ToTable("VC_Guidance");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CollegeId).HasColumnName("CollegeID");

                entity.Property(e => e.MajorId).HasColumnName("MajorID");

                entity.HasOne(d => d.College)
                    .WithMany(p => p.VcGuidance)
                    .HasForeignKey(d => d.CollegeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKVC_Guidanc491547");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.VcGuidance)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKVC_Guidanc283920");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VcGuidance)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKVC_Guidanc169535");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
