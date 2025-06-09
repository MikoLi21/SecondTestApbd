namespace SecondTestApbd.DTOs;

public class TrackRaceParticipantsDto
{
    public string RaceName { get; set; }
    public string TrackName { get; set; }
    public List<ParticipantDto> Participations { get; set; }
}