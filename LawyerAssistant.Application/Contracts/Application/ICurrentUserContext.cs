namespace Application.Contracts.Application;

public interface ICurrentUserContext
{
    int GetCurrentUserBeautyCenterId();
    string GetCurrentUserFullName();
    int GetCurrentUserId();
    string GetCurrentUsername();
    List<int> GetSecretaryExpertUserIds();
    List<int> GetExpertUserPermissionIds();
    bool IsDisableShowCustomerBirthDate();
    bool IsDisableShowCustomerMobNo();
    public List<int> GetSecretaryUserPermissionIds();
}