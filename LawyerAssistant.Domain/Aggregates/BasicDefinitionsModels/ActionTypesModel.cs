using LawyerAssistant.Domain.Base.Contracts;
using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

/// <summary>
///  اقدامات
/// </summary>
public class ActionTypesModel : ModifyDateTimeWithUserModel, IEntity
{
    protected ActionTypesModel()
    {
        
    }

    public ActionTypesModel(string title , int priority , int rememberTime)
    {
        Title = title;
        Priority = priority;
        RememberTime = rememberTime;
    }


    public void Edit(string title, int priority, int rememberTime)
    {
        Title = title;
        Priority = priority;
        RememberTime = rememberTime;
    }

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    ///  عنوان
    /// </summary>
    public string Title { get; set; }
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    ///  اولویت بندی
    ///  که می تواند اعداد تکراری  با اولویت یکسان نیز بگیرد
    /// </summary>
    public int Priority { get; set; }
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    ///   زمان یادآوری به ساعت 24 یا 72 
    /// </summary>
    public int RememberTime { get; set; }

    public ICollection<ReActionModel> Reactions { get; set; }

}
