using Domain.Base.Enums;
using LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels;
using LawyerAssistant.Domain.Aggregates.IdentitiesModels;
using LawyerAssistant.Domain.Base;
using LawyerAssistant.Domain.Base.Contracts;

namespace Domain.Aggregates.Identities
{
    public class CustomersEntity : ModifyDateTimeModel, IEntity
    {
        #region Methods
        protected CustomersEntity()
        {

        }


        // Constructor for Create
        public CustomersEntity(string mobileNumber, string firstName, string lastName, string nationalCode, DateTime birthDate, string address, int? cityId, int? provinceId)
        {
            MobileNumber = mobileNumber;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            BirthDate = birthDate;
            Address = address;
            CityId = cityId;
            ProvinceId = provinceId;
            CreateDate = DateTime.UtcNow;
            Status = Status.Active;
        }

        public void EditDetails(string mobileNumber, string firstName, string lastName, string nationalCode, DateTime birthDate, string address, int? cityId, int? provinceId)
        {
            MobileNumber = mobileNumber;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            BirthDate = birthDate;
            Address = address;
            CityId = cityId;
            ProvinceId = provinceId;
        }

        public void ChangeStatus(Status status)
        {
            Status = status;
        }

        public void RegDateAdd()
        {
            RegDateTime = DateTime.UtcNow;
        }
        public void ModDateUpdate()
        {
            ModDateTime = DateTime.UtcNow;
        }


        /// <summary>
        ///  Adding Customer to the LegalCustomers
        /// </summary>
        /// <param name="Id">Legal Id</param>
        public void AddLegal(int Id)
        { 
            LegalCompanyId = Id;
        }
        #endregion

        #region Props
        // =========================================================================
        /// <summary>
        ///  شماره موبایل
        /// </summary>
        public string MobileNumber { get; set; }
        // =========================================================================
        /// <summary>
        ///  نام
        /// </summary>
        public string FirstName { get; set; }
        // =========================================================================
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        // =========================================================================
        /// <summary>
        ///  کد ملی
        /// </summary>
        public string NationalCode { get; set; }
        // =========================================================================
        /// <summary>
        ///  کد ملی
        /// </summary>
        public DateTime BirthDate { get; set; }
        // =========================================================================
        /// <summary>
        ///  تاریخ ایجاد کابر
        /// </summary>
        public DateTime CreateDate { get; set; }
        // =========================================================================
        /// <summary>
        ///  وضعیت کاربر
        /// </summary>
        public Status Status { get; set; }
        public string Address { get; set; }
        // =========================================================================
        /// <summary>
        ///  شهر
        /// </summary>
        public int? CityId { get; set; }
        // =========================================================================
        /// <summary>
        ///  استان
        /// </summary>
        public int? ProvinceId { get; set; }
        // =========================================================================
        /// <summary>
        ///  شناسه شرکت حقوقی
        /// </summary>
        public int? LegalCompanyId { get; set; }
        public CitiesModel City { get; set; }
        public ProvincesModel Province { get; set; }
        public LegalCustomersEntity? Legal { get; set; }

        #endregion
    }
}
