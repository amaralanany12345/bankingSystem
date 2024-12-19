using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class WithdrawProcessConfiguration : IEntityTypeConfiguration<Withdraw>
    {
        public void Configure(EntityTypeBuilder<Withdraw> builder)
        {
            builder.ToTable("Withdraws").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.withdrawTime).IsRequired();
            builder.Property(a => a.valueWithdraw).IsRequired();
            builder.HasOne(a => a.account).WithMany(a => a.withdrawProcesses).HasForeignKey(a => a.accountId);
        }
    }
}
