using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SkyLux.Models;

public partial class AirlineContext : DbContext
{
    public AirlineContext()
    {
    }

    public AirlineContext(DbContextOptions<AirlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Economy> Economies { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JUFPKD7\\SQLEXPRESS;Database=airline;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.BasketId).HasName("PK__Basket__8FDA77B5F6108513");

            entity.ToTable("Basket");

            entity.HasOne(d => d.BasketTicket).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.BasketTicketId)
                .HasConstraintName("FK__Basket__BasketTi__6FE99F9F");

            entity.HasOne(d => d.BasketUser).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.BasketUserId)
                .HasConstraintName("FK__Basket__BasketUs__70DDC3D8");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contacts__5C66259B2D15E797");

            entity.Property(e => e.ContactEmail).HasMaxLength(30);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactTelephone).HasMaxLength(30);
        });

        modelBuilder.Entity<Economy>(entity =>
        {
            entity.HasKey(e => e.EconomyId).HasName("PK__Economy__C1647ECABEF83E8A");

            entity.ToTable("Economy");

            entity.Property(e => e.EconomyName).HasMaxLength(20);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDF3A152BB1A");

            entity.Property(e => e.NewsDate).HasMaxLength(20);
            entity.Property(e => e.NewsImage).HasMaxLength(30);
            entity.Property(e => e.NewsText).HasMaxLength(50);
            entity.Property(e => e.NewsTitle).HasMaxLength(30);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A7949498F58");

            entity.ToTable("Position");

            entity.Property(e => e.PositionCity).HasMaxLength(20);
            entity.Property(e => e.PositionImage).HasMaxLength(100);
            entity.Property(e => e.PositionPrice).HasMaxLength(20);
            entity.Property(e => e.PositionText).HasMaxLength(100);
            entity.Property(e => e.PositionTo).HasMaxLength(50);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__712CC6070A8D458E");

            entity.Property(e => e.TicketArrivalDate).HasColumnType("date");
            entity.Property(e => e.TicketArrivalTime).HasMaxLength(30);
            entity.Property(e => e.TicketDepartureDate).HasColumnType("date");
            entity.Property(e => e.TicketDepartureTime).HasMaxLength(30);
            entity.Property(e => e.TicketFrom).HasMaxLength(30);
            entity.Property(e => e.TicketHour).HasMaxLength(30);
            entity.Property(e => e.TicketStatus).HasMaxLength(30);
            entity.Property(e => e.TicketTo).HasMaxLength(30);

            entity.HasOne(d => d.TicketEconomy).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketEconomyId)
                .HasConstraintName("FK__Tickets__TicketE__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C01566DDA");

            entity.Property(e => e.UserBlock).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(30);
            entity.Property(e => e.UserSurname).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
