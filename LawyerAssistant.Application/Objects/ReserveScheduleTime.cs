namespace LawyerAssistant.Application.Objects;

public class ReserveScheduleTime
{
    public int BeautyCenterServiceId { get; set; }

    public int BeautyCenterId { get; set; }

    public int ServiceId { get; set; }

    public string ReserveFromTime { get; set; }

    public string ReserveToTime { get; set; }

    public string ReserveDate { get; set; } 

    public int UserId { get; set; }
}
