using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs.BaseDefinitions;

public class GetCityDTO
{
    public int Id { get; set; }
    public string Title { get; set; }

    public GenericDTO Province { get; set; }
}
