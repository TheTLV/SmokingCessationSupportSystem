using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<QuitPlan> QuitPlans { get; set; }

    public virtual DbSet<SmokingStatus> SmokingStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    // Lệnh Scaffold: dotnet ef dbcontext scaffold "Server=localhost;Database=SmokingCessationDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir . --context-dir ../DataAccessLayout --context AppDbContext --force --no-onconfiguring --use-database-names --project BusinessObjects.csproj

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SmokingCessationDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChatMess__3214EC070208E26B");

            entity.ToTable("ChatMessage");

            entity.HasIndex(e => e.ReceiverId, "IX_ChatMessage_ReceiverId");

            entity.HasIndex(e => e.SenderId, "IX_ChatMessage_SenderId");

            entity.HasIndex(e => e.SentAt, "IX_ChatMessage_SentAt");

            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ChatMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatMessa__Recei__4222D4EF");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatMessa__Sende__412EB0B6");
        });

        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coach__3214EC0758FF86A5");

            entity.ToTable("Coach");

            entity.HasIndex(e => e.UserId, "IX_Coach_UserId");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coach__UserId__3C69FB99");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC07702E49DD");

            entity.ToTable("Comment");

            entity.HasIndex(e => e.PostId, "IX_Comment_PostId");

            entity.HasIndex(e => e.UserId, "IX_Comment_UserId");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostId__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__UserId__5165187F");
        });

        modelBuilder.Entity<CommunityPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC076770D34E");

            entity.ToTable("CommunityPost");

            entity.HasIndex(e => e.CreatedAt, "IX_CommunityPost_CreatedAt");

            entity.HasIndex(e => e.UserId, "IX_CommunityPost_UserId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Community__UserI__4CA06362");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Membersh__3214EC074673B439");

            entity.ToTable("Membership");

            entity.HasIndex(e => e.UserId, "IX_Membership_UserId");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PackageName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Membershi__UserI__6477ECF3");
        });

        modelBuilder.Entity<QuitPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QuitPlan__3214EC07CD68E984");

            entity.ToTable("QuitPlan");

            entity.HasIndex(e => e.UserId, "IX_QuitPlan_UserId");

            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TargetDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.QuitPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuitPlan__UserId__44FF419A");
        });

        modelBuilder.Entity<SmokingStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SmokingS__3214EC07596F3101");

            entity.ToTable("SmokingStatus");

            entity.HasIndex(e => e.RecordDate, "IX_SmokingStatus_RecordDate");

            entity.HasIndex(e => e.UserId, "IX_SmokingStatus_UserId");

            entity.Property(e => e.CostPerPack).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.SmokingStatuses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SmokingSt__UserI__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0759023EB8");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.Username, "IX_Users_Username");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E41229796A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534857D969D").IsUnique();

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
