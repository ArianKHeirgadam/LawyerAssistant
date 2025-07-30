using LawyerAssistant.Application.DTOs.Base;
using LawyerAssistant.Application.DTOs.BaseDefinitions;
using LawyerAssistant.Application.DTOs.Identities;

namespace LawyerAssistant.Application.DTOs;

public class FilesDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsLegal { get; set; }
    public GetCustomersDTO Customer { get; set; }
    public GetLegalCustomerDetailsDTO Legal { get; set; }
    public GetDemandsDTO Demand { get; set; }
}
