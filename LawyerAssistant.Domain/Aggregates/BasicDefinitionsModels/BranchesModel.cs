using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

/// <summary>
///  شعبه ها
/// </summary>
public class BranchesModel : IdentifierModel , IEntity
{
    protected BranchesModel()
    {
        
    }
    public BranchesModel( string title , int complexId)
    {
        Title = title;
        ComplexId = complexId;
    }

    public void Edit(string title, int complexId)
    {
        Title = title;
        ComplexId = complexId;
    }

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    ///  عنوان
    /// </summary>
    public string Title { get; set; }
    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>
    ///  شناسه مجتمع
    /// </summary>
    public int ComplexId { get; set; }
    public ComplexesModel Complexe { get; set; }
    public ICollection<ReActionModel> Reactions { get; set; }

}
