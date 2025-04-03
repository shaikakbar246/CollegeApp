using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired(false);
            builder.HasData(new List<Department>()
           {
               new Department
            {
                Id = 1,
                DepartmentName="MPC",
                Description="MPC Department",
            },
            new Department
            {
                Id = 2,
                DepartmentName="CEC",
                Description="CEC Department",
            }
           });
        }
    }
}
