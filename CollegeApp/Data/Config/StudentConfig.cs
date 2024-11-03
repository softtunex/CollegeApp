using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class StudentConfig:IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "Test1",
                    Address="Nigeria",
                    Email="Test1@gmail.com",
                    DOB= new DateTime(2022,12,12).ToString("yyyy-MM-dd"),
                },new Student
                {
                    Id = 2,
                    StudentName = "Test2",
                    Address="Nigeria",
                    Email="Test2@gmail.com",
                    DOB= new DateTime(2022,12,12).ToString("yyyy-MM-dd"),
                },

            });
        }
    }
}
