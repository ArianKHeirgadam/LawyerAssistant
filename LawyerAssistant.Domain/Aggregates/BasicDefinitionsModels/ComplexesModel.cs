using LawyerAssistant.Domain.Base.Contracts;
using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;

public class ComplexesModel : IdentifierModel, IEntity
{
    protected ComplexesModel()
    {

    }
    public ComplexesModel(string title, int cityId)
    {
        Title = title;
        CityId = cityId;
        Branches = new HashSet<BranchesModel>();
    }

    public void Edit(string title, int cityId)
    {
        Title = title;
        CityId = cityId;
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
    public int CityId { get; set; }
    public CitiesModel City { get; set; }
    public ICollection<BranchesModel> Branches { get; set; }
}
