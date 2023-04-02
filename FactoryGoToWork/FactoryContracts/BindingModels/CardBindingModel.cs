using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
    public class CardBindingModel : ICardModel
    {
        public int Id { get; set; }

        public int ClientID { get; set; }

        public int AccountId { get; set; }

        public string Number { get; set; } = string.Empty;

        public string CVC { get; set; } = string.Empty;

        public DateTime Period { get; set; } = DateTime.Now;
    }
}
