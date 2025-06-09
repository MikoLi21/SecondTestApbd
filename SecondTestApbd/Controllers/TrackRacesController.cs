namespace SecondTestApbd.Controllers;
using SecondTestApbd.Data;
using SecondTestApbd.DTOs;
using SecondTestApbd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/track-races")]
[ApiController]
public class TrackRacesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TrackRacesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipants([FromBody] TrackRaceParticipantsDto dto)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == dto.RaceName);
        if (race == null)
            return NotFound(new { message = "Race not found" });

        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == dto.TrackName);
        if (track == null)
            return NotFound(new { message = "Track not found" });

        var trackRace = await _context.TrackRaces
            .Include(tr => tr.RaceParticipations)
            .FirstOrDefaultAsync(tr => tr.RaceId == race.RaceId && tr.TrackId == track.TrackId);

        if (trackRace == null)
            return NotFound(new { message = "TrackRace relation not found" });

        foreach (var p in dto.Participations)
        {
            var racer = await _context.Racers.FindAsync(p.RacerId);
            if (racer == null)
                return NotFound(new { message = $"Racer {p.RacerId} not found" });

            var existing = await _context.RaceParticipations
                .FirstOrDefaultAsync(rp => rp.TrackRaceId == trackRace.TrackRaceId && rp.RacerId == p.RacerId);

            if (existing == null)
            {
                _context.RaceParticipations.Add(new RaceParticipation
                {
                    TrackRaceId = trackRace.TrackRaceId,
                    RacerId = p.RacerId,
                    Position = p.Position,
                    FinishTimeInSeconds = p.FinishTimeInSeconds
                });
            }
            else if (p.FinishTimeInSeconds < existing.FinishTimeInSeconds)
            {
                existing.FinishTimeInSeconds = p.FinishTimeInSeconds;
                existing.Position = p.Position;
            }

            if (trackRace.BestTimeInSeconds == null || p.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
            {
                trackRace.BestTimeInSeconds = p.FinishTimeInSeconds;
            }
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}
