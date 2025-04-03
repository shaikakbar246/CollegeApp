using CollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }
       public DbSet<Student> Students { get; set; }
       public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //instead of writing here we can create seperate
            //Entity type Configuration file for each table in Config folder of StudentConfig.cs file
            // modelBuilder.Entity<Student>().HasData(new List<Student>()
            //{
            //    new Student
            // {
            //     Id = 1,
            //     StudentName="Akbar",
            //     Address="Bhimavaram",
            //     Email="Akbar@gmail.com",
            //     DOE=new DateTime(2022,12,12)
            // },
            // new Student
            // {
            //     Id = 2,
            //     StudentName="Shajahan",
            //     Address="Jaggayyapeta",
            //     Email="Shajahan@gmail.com",
            //     DOE=new DateTime(2021,12,12)
            // }
            //});

            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(n=>n.StudentName).IsRequired();
            //    entity.Property(n=>n.StudentName).HasMaxLength(250);
            //    entity.Property(n=>n.Address).IsRequired(false).HasMaxLength(500);
            //    entity.Property(n=>n.Email).IsRequired().HasMaxLength(250);

            //});
            //Student Table
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
        }
    }
}
