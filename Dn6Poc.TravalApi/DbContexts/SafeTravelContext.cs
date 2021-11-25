using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Dn6Poc.TravalApi.Models;

namespace Dn6Poc.TravalApi.DbContexts
{
    public partial class SafeTravelContext : DbContext
    {
        public SafeTravelContext()
        {
        }

        public SafeTravelContext(DbContextOptions<SafeTravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:SafeTravel");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.HasIndex(e => e.Username, "IX_AppUser")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("BlogPost");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_BlogPost_Blog");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasIndex(e => e.CountryName, "IX_Country")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
