using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class DepositProcessConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.ToTable("Deposits").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.depositTime).IsRequired();
            builder.Property(a => a.valueDeposit).IsRequired();
            builder.HasOne(a=>a.account).WithMany(a=>a.depositProcesses).HasForeignKey(a=>a.accountId);
        }
    }
}
