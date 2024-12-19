﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using banking;

#nullable disable

namespace banking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241010092020_updateAccount")]
    partial class updateAccount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("banking.Models.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("accountNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("accountType")
                        .HasColumnType("int");

                    b.Property<int>("balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("id");

                    b.HasIndex("customerId");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("banking.Models.AnnualDeposit", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("AnnualDepositPeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("AnnualDepositRepaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AnnualDepositRepaymentValue")
                        .HasColumnType("int");

                    b.Property<DateTime>("AnnualDepositRequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AnnualDepositValue")
                        .HasColumnType("int");

                    b.Property<bool>("acceptAnnualDeposit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("accountId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("accountId");

                    b.ToTable("AnnualDeposits", (string)null);
                });

            modelBuilder.Entity("banking.Models.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("identityNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.HasKey("id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("banking.Models.Deposit", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("accountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("depositTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("valueDeposit")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("accountId");

                    b.ToTable("Deposits", (string)null);
                });

            modelBuilder.Entity("banking.Models.Finance", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("FinancePeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinanceRepaymentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinanceRepaymentValue")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinanceRequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinanceValue")
                        .HasColumnType("int");

                    b.Property<bool>("acceptFinance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("accountId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("accountId");

                    b.ToTable("finances", (string)null);
                });

            modelBuilder.Entity("banking.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("identityNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.HasKey("id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("banking.Models.Withdraw", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("accountId")
                        .HasColumnType("int");

                    b.Property<int>("valueWithdraw")
                        .HasColumnType("int");

                    b.Property<DateTime>("withdrawTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("accountId");

                    b.ToTable("Withdraws", (string)null);
                });

            modelBuilder.Entity("banking.Models.Account", b =>
                {
                    b.HasOne("banking.Models.Customer", "customer")
                        .WithMany("customerAccounts")
                        .HasForeignKey("customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("banking.Models.AnnualDeposit", b =>
                {
                    b.HasOne("banking.Models.Account", "account")
                        .WithMany("AnnualDeposits")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("banking.Models.Deposit", b =>
                {
                    b.HasOne("banking.Models.Account", "account")
                        .WithMany("depositProcesses")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("banking.Models.Finance", b =>
                {
                    b.HasOne("banking.Models.Account", "account")
                        .WithMany("Finances")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("banking.Models.Withdraw", b =>
                {
                    b.HasOne("banking.Models.Account", "account")
                        .WithMany("withdrawProcesses")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");
                });

            modelBuilder.Entity("banking.Models.Account", b =>
                {
                    b.Navigation("AnnualDeposits");

                    b.Navigation("Finances");

                    b.Navigation("depositProcesses");

                    b.Navigation("withdrawProcesses");
                });

            modelBuilder.Entity("banking.Models.Customer", b =>
                {
                    b.Navigation("customerAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}