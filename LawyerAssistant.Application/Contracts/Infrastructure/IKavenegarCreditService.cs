using LawyerAssistant.Application.DTOs;


namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IKavenegarCreditService
{
    Task<SmsCreditInquiryDTOModel> Inquiry(string apiKey);
}