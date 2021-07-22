using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Entities.Models
{
    public partial class SaleManagerContext : DbContext
    {
        public SaleManagerContext()
        {
        }

        public SaleManagerContext(DbContextOptions<SaleManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionName).HasMaxLength(255);

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.GroupName).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Tài khoản");

                entity.Property(e => e.AccountType).HasComment("Loại tài khoản");

                entity.Property(e => e.Active).HasComment("Trạng thái sử dụng");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("Địa chỉ");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasComment("Avatar");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sinh");

                entity.Property(e => e.CreateBy).HasComment("Người thêm mới");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày thêm mới");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Mô tả thêm");

                entity.Property(e => e.EditBy).HasComment("Người chỉnh sửa");

                entity.Property(e => e.EditDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasComment("Email");

                entity.Property(e => e.Gender).HasComment("Giới tính");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Mật khẩu");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .HasComment("Số điện thoại");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("Họ tên");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
