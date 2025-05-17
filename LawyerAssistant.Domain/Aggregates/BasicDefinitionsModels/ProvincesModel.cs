using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class ProvincesModel :  ModifyDateTimeWithUserModel, IEntity
    {
        protected ProvincesModel()
        {
        }
        public ProvincesModel( string name)
        {
            Name = name;
            IsActive = true;
            RegDateTime = DateTime.UtcNow;
            Cities = new HashSet<CitiesModel>();
            Customers = new HashSet<CustomersEntity>();
        }
        public void ChangeActivation(bool isActive)
        {
            IsActive = isActive;
        }
        public void Edit(string name)
        {
            Name = name;
            ModDateTime = DateTime.UtcNow;
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
        public ICollection<CustomersEntity> Customers { get; set; }

    }
}
