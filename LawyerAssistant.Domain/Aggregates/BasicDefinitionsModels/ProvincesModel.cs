using Domain.Aggregates.Identities;
using LawyerAssistant.Domain.Base.Contracts;

namespace LawyerAssistant.Domain.Aggregates.BasicDefinitionsModels
{
    public class ProvincesModel : IEntity
    {
        public Guid Id { get; set; }
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
        public ICollection<CustomersModel> Customers { get; set; }

    }
}
