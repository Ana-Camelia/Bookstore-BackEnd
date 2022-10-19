using Bookstore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DataAccess.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.HasOne(employee => employee.Role)
                .WithMany(role => role.Employees)
                .HasForeignKey(employee => employee.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
