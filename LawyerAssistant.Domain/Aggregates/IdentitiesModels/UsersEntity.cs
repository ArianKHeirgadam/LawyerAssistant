using Domain.Base.Enums;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels.Enums;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace Domain.Aggregates.Identities;

public class UsersEntity : ModifyDateTimeWithUserModel, IEntity
{
    #region Methods
    protected UsersEntity()
    {
        
    }

    public UsersEntity(int id , string username,string firstName, string lastName, string passwordHash, bool gender, string picPath, UserRole role)
    {
        Id = id;
        UserName = username;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        Gender = gender;
        PicPath = picPath;
        Role = role;
        Status = Status.Active;
    }

    public void ChangeStatus(Status status, int? userId)
    { 
        Status = status;
        if(userId.HasValue)
            Modifier(userId.Value);
    }
    public void ChangePassword(string newPassword , int userId)
    {
        PasswordHash = newPassword;
        Modifier(userId);
    }
    private void Modifier(int userId)
    {
        ModDateTime = DateTime.UtcNow;
    }
    public void Edit(string firstName, string lastName, string userName, bool gender, string picPath, UserRole role , int ModifierUser)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        PicPath = picPath;
        Role = role;
        Modifier(ModifierUser);
    }

    #endregion


    #region Props
    /// <summary>
    /// نام کاربری
    /// </summary>
    public string UserName { get; set; }
    //*******************************************************************
    /// <summary>
    ///  نام
    /// </summary>
    public string FirstName { get; set; }
    //*******************************************************************
    /// <summary>
    ///  جنسیت
    /// </summary>
    public bool Gender { get; set; }
    //*******************************************************************
    /// <summary>
    ///   نام خانوادگی
    /// </summary>
    public string LastName { get; set; }
    //*******************************************************************
    /// <summary>
    ///  رمز عبور 
    /// </summary>
    public string PasswordHash { get; set; }
    //*******************************************************************
    /// <summary>
    ///  وضعیت
    /// </summary>
    public Status Status { get; set; }
    //*******************************************************************
    /// <summary>
    ///  تصویر پروفایل
    /// </summary>
    public string? PicPath { get; set; }

    //============================================
    /// <summary>
    ///  نقش کاربر.
    /// </summary>
    public UserRole Role { get; set; }
    #endregion

}
