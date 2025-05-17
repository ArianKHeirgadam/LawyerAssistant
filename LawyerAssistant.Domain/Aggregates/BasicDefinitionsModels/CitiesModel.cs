using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class CitiesModel : ModifyDateTimeWithUserModel, IEntity
    {
        protected CitiesModel()
        {

        }

        public CitiesModel(string name, int provinceId)
        {
            Name = name;
            ProvinceId = provinceId;
            IsActive = true;
            RegDateTime = DateTime.UtcNow;
            Customers = new HashSet<CustomersEntity>();
        }

        public void Edit(string name, int provinceId)
        {
            Name = name;
            ProvinceId = provinceId;
            ModDateTime = DateTime.UtcNow;
        }

        public void ChangeActivation(bool isActive)
        {
            IsActive = isActive;
        }

        /// <summary>
        ///  فعال
        /// </summary>
        public bool IsActive { get; set; }
        //===========================================================
        /// <summary>
        ///  نام شهر
        /// </summary>
        public string Name { get; set; }
        //===========================================================ارتباطات
        /// <summary>
        ///  استان
        /// </summary>
        public int ProvinceId { get; set; }
        public ProvincesModel Province { get; set; }

        public ICollection<CustomersEntity> Customers { get; set; }
    }
}
