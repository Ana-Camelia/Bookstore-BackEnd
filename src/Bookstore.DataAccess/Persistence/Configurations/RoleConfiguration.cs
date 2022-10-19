using Bookstore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Numerics;

namespace Bookstore.DataAccess.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.Id);

            builder.HasData(
                new[]
                {
                    new Role
                    {
                        Id=Guid.Parse("c5e45b7d-d3e9-4097-b933-5c7e4eb858fb"),
                        Name = "Admin"
                    },
                    new Role
                    {
                        Id=Guid.Parse("dcc6be20-6c76-4f4e-8010-c6198d26ca7e"),
                        Name = "Cashier"
                    }
                });
        }
    }
}
