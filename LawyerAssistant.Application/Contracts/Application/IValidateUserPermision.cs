namespace LawyerAssistant.Application.Contracts.Application;

public interface IValidateUserPermision 
{
    Task Validate(int userId);
}