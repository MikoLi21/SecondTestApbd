namespace SecondTestApbd.Data;
using Microsoft.EntityFrameworkCore;
using SecondTestApbd.Models;

public class AppDbContext : DbContext
{
    public DbSet<Racer> Racers { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RaceParticipation>()
            .HasKey(rp => new { rp.TrackRaceId, rp.RacerId });


        modelBuilder.Entity<RaceParticipation>()
            .HasOne(rp => rp.TrackRace)
            .WithMany(tr => tr.RaceParticipations)
            .HasForeignKey(rp => rp.TrackRaceId);

        modelBuilder.Entity<RaceParticipation>()
            .HasOne(rp => rp.Racer)
            .WithMany(r => r.RaceParticipations)
            .HasForeignKey(rp => rp.RacerId);

        modelBuilder.Entity<TrackRace>()
            .HasOne(tr => tr.Track)
            .WithMany(t => t.TrackRaces)
            .HasForeignKey(tr => tr.TrackId);

        modelBuilder.Entity<TrackRace>()
            .HasOne(tr => tr.Race)
            .WithMany(r => r.TrackRaces)
            .HasForeignKey(tr => tr.RaceId);
    }
}