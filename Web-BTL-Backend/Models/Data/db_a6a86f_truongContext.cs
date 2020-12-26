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

        public virtual DbSet<Auths> Auths { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<ConversationReply> ConversationReply { get; set; }
        public virtual DbSet<Conversations> Conversations { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<FavoriteRoom> FavoriteRoom { get; set; }
        public virtual DbSet<Motelrooms> Motelrooms { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RoomImages> RoomImages { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Utilities> Utilities { get; set; }

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
            modelBuilder.Entity<Auths>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PRIMARY");

                entity.ToTable("auths");

                entity.HasIndex(e => e.IdUser)
                    .HasName("id_user_idx");

                entity.Property(e => e.UserName)
                    .HasColumnName("user_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("id_category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CateroryName)
                    .IsRequired()
                    .HasColumnName("cateroryName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date");

                entity.Property(e => e.Slug)
                    .HasColumnName("slug")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("date");
            });

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

            modelBuilder.Entity<ConversationReply>(entity =>
            {
                entity.HasKey(e => e.IdCr)
                    .HasName("PRIMARY");

                entity.ToTable("conversation_reply");

                entity.Property(e => e.IdCr)
                    .HasColumnName("id_cr")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdC)
                    .HasColumnName("id_c")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ReplyContent)
                    .HasColumnName("replyContent")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Conversations>(entity =>
            {
                entity.HasKey(e => e.IdC)
                    .HasName("PRIMARY");

                entity.ToTable("conversations");

                entity.Property(e => e.IdC)
                    .HasColumnName("id_c")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser1)
                    .HasColumnName("id_user1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser2)
                    .HasColumnName("id_user2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("PRIMARY");

                entity.ToTable("districts");

                entity.Property(e => e.IdDistrict)
                    .HasColumnName("id_district")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date");

                entity.Property(e => e.DistrictName)
                    .HasColumnName("districtName")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Slug)
                    .HasColumnName("slug")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<FavoriteRoom>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdRoom })
                    .HasName("PRIMARY");

                entity.ToTable("favorite_room");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Motelrooms>(entity =>
            {
                entity.HasKey(e => e.IdRoom)
                    .HasName("PRIMARY");

                entity.ToTable("motelrooms");

                entity.HasIndex(e => e.IdDistrict)
                    .HasName("id_district");

                entity.HasIndex(e => e.IdUtility)
                    .HasName("id_utility");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("int(11)");

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

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expire_date")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Reports>(entity =>
            {
                entity.HasKey(e => e.IdReport)
                    .HasName("PRIMARY");

                entity.ToTable("reports");

                entity.Property(e => e.IdReport)
                    .HasColumnName("id_report")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.IdRole)
                    .HasColumnName("id_role")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<RoomImages>(entity =>
            {
                entity.HasKey(e => e.IdImage)
                    .HasName("PRIMARY");

                entity.ToTable("room_images");

                entity.HasIndex(e => e.IdRoom)
                    .HasName("id_room");

                entity.Property(e => e.IdImage)
                    .HasColumnName("id_image")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRoom)
                    .HasColumnName("id_room")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasColumnName("image_path")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.RoomImages)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("room_images_ibfk_1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasIndex(e => e.IdRole)
                    .HasName("id_role_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("id_user_index");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'no-avatar.jpg'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.IdRole)
                    .HasColumnName("id_role")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_role");
            });

            modelBuilder.Entity<Utilities>(entity =>
            {
                entity.HasKey(e => e.IdUtility)
                    .HasName("PRIMARY");

                entity.ToTable("utilities");

                entity.Property(e => e.IdUtility)
                    .HasColumnName("id_utility")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AirConditioning)
                    .HasColumnName("airConditioning")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Balcony)
                    .HasColumnName(@"
balcony")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BathRoom)
                    .HasColumnName("bath_room")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ElectricityWater)
                    .HasColumnName("electricity_water")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Kitchen)
                    .HasColumnName("kitchen")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UtilityOthers)
                    .HasColumnName("utility_others")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Wifi)
                    .HasColumnName("wifi")
                    .HasColumnType("int(11)");
            });
        }
    }
}
