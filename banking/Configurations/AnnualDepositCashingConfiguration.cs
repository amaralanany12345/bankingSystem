using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class AnnualDepositCashingConfiguration : IEntityTypeConfiguration<AnnualDepositCashing>
    {
        public void Configure(EntityTypeBuilder<AnnualDepositCashing> builder)
        {
            builder.ToTable("annualDepositCashing").HasKey(a => a.id);
            builder.Property(a=>a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a=>a.annualDepositCashingTime).IsRequired();
            builder.Property(a=>a.annualDepositCashingValue).IsRequired();
            builder.HasOne(a=>a.annualDeposit).WithMany(a=>a.annualDepositCashing).HasForeignKey(a=>a.annualDepositId);
        }
    }
}
