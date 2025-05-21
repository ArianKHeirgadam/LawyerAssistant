using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class CitiesModel : IdentifierModel, IEntity
    {
        protected CitiesModel()
        {

        }

        public CitiesModel(string name, int provinceId)
        {
            Name = name;
            ProvinceId = provinceId;
            IsActive = true;
            Customers = new HashSet<CustomersModel>();
            Complexes = new HashSet<ComplexesModel>();
        }

        public void Edit(string name, int provinceId)
        {
            Name = name;
            ProvinceId = provinceId;
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
        public ICollection<ComplexesModel> Complexes { get; set; }
        public ICollection<CustomersModel> Customers { get; set; }
    }
}
