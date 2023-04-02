using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class CreditingViewModel : ICreditingModel 
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        [DisplayName("Номер карты")]
        public string? CardNumber { get; set; }

        [DisplayName("Сумма операции")]
        public int Sum { get; set; }

        [DisplayName("Дата операции")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
