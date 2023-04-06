using FactoryDataModels.Models;

namespace FactoryContracts.SearchModels
{
    public class LatheBusySearchModel
    {
        public int? Id { get; set; }

        public int? Percent { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? DateFrom { get; set; }
    }
}
