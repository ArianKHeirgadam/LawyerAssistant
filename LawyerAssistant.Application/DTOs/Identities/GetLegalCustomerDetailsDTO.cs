using LawyerAssistant.Application.DTOs.Base;

namespace LawyerAssistant.Application.DTOs.Identities;
public class GetLegalCustomerDetailsDTO
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string LegalNationalCode { get; set; }
    public string Address { get; set; }
    public List<GenericDTO> Customers { get; set; }
}

