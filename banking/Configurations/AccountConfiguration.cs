using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts").HasKey(a => a.id);
            builder.Property(a=>a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.accountNumber).IsRequired().HasMaxLength(14);
            builder.Property(a => a.balance).IsRequired().HasDefaultValue(0);
            builder.Property(a => a.accountType).IsRequired();
            builder.Property(a => a.isActive).IsRequired().HasDefaultValue(true);
            builder.HasOne(a=>a.customer).WithMany(a=>a.customerAccounts).HasForeignKey(a=>a.customerId);
        }
    }
}
