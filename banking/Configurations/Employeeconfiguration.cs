using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees").HasKey(a=>a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a=>a.email).IsRequired();
            builder.Property(a => a.userName).IsRequired().HasMaxLength(225);
            builder.Property(a => a.email).IsRequired();
            builder.HasIndex(a => a.email).IsUnique();
            builder.Property(a => a.password).IsRequired();
            builder.Property(a => a.phone).IsRequired().HasMaxLength(100);
            builder.Property(a => a.age).IsRequired();
            builder.Property(a => a.identityNumber).IsRequired().HasMaxLength(14);
            builder.Property(a=>a.role).IsRequired();
        }
    }
}
