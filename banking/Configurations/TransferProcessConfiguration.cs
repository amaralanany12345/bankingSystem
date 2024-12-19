using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class TransferProcessConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("transfers");
            builder.HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd(); 
            builder.Property(a=>a.transferValue).IsRequired();
            builder.Property(a=>a.transferTime).IsRequired();
            builder.HasOne(a=>a.sentAccount).WithMany(a=>a.transferToProcesses).HasForeignKey(a=>a.sentAccountId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a=>a.receivedAccount).WithMany(a=>a.transferFromProcesses).HasForeignKey(a=>a.receivedAccountId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
