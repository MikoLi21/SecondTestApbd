namespace SecondTestApbd.DTOs;

public class RacerParticipationDto
{
    public int RacerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<ParticipationDto> Participations { get; set; }
}