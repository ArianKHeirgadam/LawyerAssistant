using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class ProvincesModel :  ModifyDateTimeWithUserModel
    {
        protected ProvincesModel()
        {
        }
        public ProvincesModel( string name)
        {
            Name = name;
            IsActive = true;
            Cities = new HashSet<CitiesModel>();
        }
        public void ChangeActivation(bool isActive)
        {
            IsActive = isActive;
        }
        public void Edit(string name)
        {
            Name = name;
        }
        //======================================================
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        //=======================================================
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        //=======================================================
        /// <summary>
        /// 
        /// </summary>
        public ICollection<CitiesModel> Cities { get; set; }

    }
}
