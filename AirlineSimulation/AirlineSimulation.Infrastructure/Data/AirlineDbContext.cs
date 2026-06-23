using Microsoft.EntityFrameworkCore;
using AirlineSimulation.Domain.Entities;
using AirlineSimulation.Domain.Enums;

namespace AirlineSimulation.Infrastructure.Data;

public class AirlineDbContext : DbContext
{
    public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; } = null!;
    public DbSet<Passenger> Passengers { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<BaggageTag> BaggageTags { get; set; } = null!;
    
    // Customs System
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<AirportCongestion> AirportCongestions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Flight Configuration
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(f => f.FlightId);
            entity.Property(f => f.FlightNumber).IsRequired().HasMaxLength(20);
            entity.HasIndex(f => f.FlightNumber).IsUnique();
            entity.Property(f => f.DepartureAirport).IsRequired().HasMaxLength(100);
            entity.Property(f => f.ArrivalAirport).IsRequired().HasMaxLength(100);
            entity.Property(f => f.Terminal).HasMaxLength(10);
            entity.Property(f => f.Gate).HasMaxLength(10);
            entity.Property(f => f.FlightStatus).HasConversion<string>();
        });

        // Passenger Configuration
        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(p => p.PassengerId);
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.PassportNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(p => p.PassportNumber).IsUnique();
            entity.Property(p => p.Nationality).IsRequired().HasMaxLength(100);
            entity.Property(p => p.PassengerRole).HasConversion<string>();
        });

        // Ticket Configuration
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.TicketId);
            entity.Property(t => t.TicketNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(t => t.TicketNumber).IsUnique();
            entity.HasIndex(t => new { t.FlightId, t.PassengerId });
            entity.Property(t => t.SeatNumber).IsRequired().HasMaxLength(10);
            entity.Property(t => t.TravelClass).HasConversion<string>();
            entity.Property(t => t.Terminal).HasMaxLength(10);
            entity.Property(t => t.Gate).HasMaxLength(10);
            entity.Property(t => t.BarcodeData).HasMaxLength(500);
            entity.Property(t => t.BoardingStatus).HasConversion<string>();

            // Relationships
            entity.HasOne(t => t.Flight)
                  .WithMany(f => f.Tickets)
                  .HasForeignKey(t => t.FlightId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Passenger)
                  .WithMany(p => p.Tickets)
                  .HasForeignKey(t => t.PassengerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // BaggageTag Configuration
        modelBuilder.Entity<BaggageTag>(entity =>
        {
            entity.HasKey(b => b.BaggageTagId);
            entity.Property(b => b.TagNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(b => b.TagNumber).IsUnique();
            entity.Property(b => b.WeightKg).HasColumnType("decimal(5,2)");
            entity.Property(b => b.CurrentLocation).HasMaxLength(50);

            // Relationship
            entity.HasOne(b => b.Ticket)
                  .WithMany(t => t.BaggageTags)
                  .HasForeignKey(b => b.TicketId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Customs System Configurations
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CustomsRate).HasColumnType("decimal(5,4)");
            entity.HasIndex(e => e.Name);
            entity.HasOne(e => e.SubCategory)
                .WithMany(s => s.Products)
                .HasForeignKey(e => e.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // AirportCongestion Configuration
        modelBuilder.Entity<AirportCongestion>(entity =>
        {
            entity.HasKey(c => c.AirportCongestionId);
            entity.Property(c => c.AirportIataCode).IsRequired().HasMaxLength(10);
            
            // جعل التركيبة ثنائية فريدة لمنع تكرار نفس المطار ونفس اليوم والساعة
            entity.HasIndex(c => new { c.AirportIataCode, c.DayOfWeek, c.HourOfDay }).IsUnique();
            entity.Property(c => c.AverageCongestion).HasColumnType("decimal(5,2)");
        });
    }
}
