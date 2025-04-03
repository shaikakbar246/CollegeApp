using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.StudentName).IsRequired();
            builder.Property(x => x.StudentName).HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.HasData(new List<Student>()
           {
               new Student
            {
                Id = 1,
                StudentName="Akbar",
                Address="Bhimavaram",
                Email="Akbar@gmail.com",
                DOE=new DateTime()
            },
            new Student
            {
                Id = 2,
                StudentName="Shajahan",
                Address="Jaggayyapeta",
                Email="Shajahan@gmail.com",
                DOE=new DateTime()
            }
           });

            builder.HasOne(n => n.Department)
                .WithMany(n => n.Students)
                .HasForeignKey(n => n.DepartmentId)
                .HasConstraintName("FK_Students_Department");
        }
    }
}
