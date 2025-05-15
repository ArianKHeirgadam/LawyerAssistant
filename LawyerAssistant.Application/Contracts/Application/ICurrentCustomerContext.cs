namespace LawyerAssistant.Application.Contracts.Application;

public interface ICurrentCustomerContext
{
    int GetCurrentCustomerId();
    string GetCurrentCustomerName();
}