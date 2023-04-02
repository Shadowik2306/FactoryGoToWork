using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
    public class CreditingSearchModel
    {
        public int? Id { get; set; }

        public int? CardId { get; set; }

        public int? Sum { get; set; }

        public DateTime? date { get; set; }
    }
}
