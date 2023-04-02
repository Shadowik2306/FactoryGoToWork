using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.BindingModels
{
    public class DebitingBindingModel : IDebitingModel
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public int Sum { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
