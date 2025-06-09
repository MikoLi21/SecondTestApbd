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
        modelBuilder.Entity<Racer>().HasData(
            new Racer { RacerId = 1, FirstName = "Lewis", LastName = "Hamilton" },
            new Racer { RacerId = 2, FirstName = "Diana", LastName = "Joni" }
        );

        modelBuilder.Entity<Race>().HasData(
            new Race { RaceId = 1, Name = "British Grand Prix", Location = "Silverstone, UK", Date = new DateTime(2025, 7, 14) },
            new Race { RaceId = 2, Name = "Monaco Grand Prix", Location = "Monte Carlo, Monaco", Date = new DateTime(2025, 4, 21) }
        );

        modelBuilder.Entity<Track>().HasData(
            new Track { TrackId = 1, Name = "Silverstone Circuit", LengthInKm = 5.89m },
            new Track { TrackId = 2, Name = "Monaco Circuit", LengthInKm = 4.54m }
        );

        modelBuilder.Entity<TrackRace>().HasData(
            new TrackRace { TrackRaceId = 1, TrackId = 1, RaceId = 1, Laps = 52, BestTimeInSeconds = 5400 },
            new TrackRace { TrackRaceId = 2, TrackId = 2, RaceId = 2, Laps = 78, BestTimeInSeconds = 6000 }
        );

        modelBuilder.Entity<RaceParticipation>().HasData(
            new RaceParticipation { TrackRaceId = 1, RacerId = 1, FinishTimeInSeconds = 5460, Position = 1 },
            new RaceParticipation { TrackRaceId = 2, RacerId = 1, FinishTimeInSeconds = 6250, Position = 2 }
        );
    }
}

    