namespace SecondTestApbd.Controllers;
using Microsoft.AspNetCore.Mvc;
using SecondTestApbd.Data;
using SecondTestApbd.DTOs;

using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RacersController : ControllerBase
{
    private readonly AppDbContext _context;

    public RacersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/participations")]
    public async Task<IActionResult> GetRacerParticipations(int id)
    {
        var racer = await _context.Racers
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.TrackRace)
            .ThenInclude(tr => tr.Race)
            .Include(r => r.RaceParticipations)
            .ThenInclude(rp => rp.TrackRace)
            .ThenInclude(tr => tr.Track)
            .FirstOrDefaultAsync(r => r.RacerId == id);

        if (racer == null)
            return NotFound();
        
        var result = new RacerParticipationDto
        {
            RacerId = racer.RacerId,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            Participations = racer.RaceParticipations.Select(rp => new ParticipationDto
            {
                Race = new RaceDto
                {
                    Name = rp.TrackRace.Race.Name,
                    Location = rp.TrackRace.Race.Location,
                    Date = rp.TrackRace.Race.Date
                },
                Track = new TrackDto
                {
                    Name = rp.TrackRace.Track.Name,
                    LengthInKm = rp.TrackRace.Track.LengthInKm
                },
                Laps = rp.TrackRace.Laps,
                FinishTimeInSeconds = rp.FinishTimeInSeconds,
                Position = rp.Position
            }).ToList()
        };

        return Ok(result);
    }
}
