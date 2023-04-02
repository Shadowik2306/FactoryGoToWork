using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.SearchModels
{
    public class CardSearchModel
    {
        public int? Id { get; set; }

        public int? ClientID { get; set; }

        public int? AccountId { get; set; }

        public string? Number { get; set; }

        public string? CVC { get; set; }

        public DateTime? Period { get; set; }
    }
}
