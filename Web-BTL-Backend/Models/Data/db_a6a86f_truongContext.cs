using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Web_BTL_Backend.Models.Data
{
    public partial class db_a6a86f_truongContext : DbContext
    {
        public db_a6a86f_truongContext()
        {
        }

        public db_a6a86f_truongContext(DbContextOptions<db_a6a86f_truongContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Motelrooms> Motelrooms { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'auths'. Please see the warning messages.
        // Unable to generate entity type for table 'categories'. Please see the warning messages.
        // Unable to generate entity type for table 'conversation_reply'. Please see the warning messages.
        // Unable to generate entity type for table 'conversations'. Please see the warning messages.
        // Unable to generate entity type for table 'districts'. Please see the warning messages.
        // Unable to generate entity type for table 'favorite_room'. Please see the warning messages.
        // Unable to generate entity type for table 'images'. Please see the warning messages.
        // Unable to generate entity type for table 'reports'. Please see the warning messages.
        // Unable to generate entity type for table 'roles'. Please see the warning messages.
        // Unable to generate entity type for table 'user_roles'. Please see the warning messages.
        // Unable to generate entity type for table 'utilities'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=MYSQL5032.site4now.net;Database=db_a6a86f_truong;Uid=a6a86f_truong;Pwd=tg@Xz47w@h9G-jK");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("PRIMARY");

                entity.ToTable("comments");

                entity.HasIndex(e => e.IdUser)
                    .HasName("id_user");

                entity.Property(e => e.IdComment)
                    .HasColumnName("id_comment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date");

                entity.Property(e => e.IdPost)
                    .HasColumnName("id_post")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Motelrooms>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PRIMARY");

                entity.ToTable("motelrooms");

                entity.HasIndex(e => e.IdDistrict)
                    .HasName("id_district");

                entity.Property(e => e.IdRoom).HasColumnName("id_room");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("id_category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdDistrict)
                    .HasColumnName("id_district")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IdUtility)
                    .IsRequired()
                    .HasColumnName("id_utility")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsGeneral)
                    .HasColumnName("isGeneral")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnName("position")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Views)
                    .HasColumnName("views")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.IdPost)
                    .HasName("PRIMARY");

                entity.ToTable("posts");

                entity.Property(e => e.IdPost)
                    .HasColumnName("id_post")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'no-avatar.jpg'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });
        }
    }
}
