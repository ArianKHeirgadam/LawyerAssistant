using LawyerAssistant.Domain.Base;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class CitiesModel : ModifyDateTimeWithUserModel
    {
        protected CitiesModel()
        {

        }

        public CitiesModel(string name, int provinceId)
        {
            Name = name;
            ProvinceId = provinceId;
            IsActive = true;
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
    }
}
