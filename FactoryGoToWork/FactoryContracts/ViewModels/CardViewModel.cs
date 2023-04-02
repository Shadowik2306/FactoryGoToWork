using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryContracts.ViewModels
{
    public class CardViewModel : ICardModel 
    {
        public int Id { get; set; }

        public int ClientID { get; set; }

        [DisplayName("Фамилия клиента")]
        public string? ClientSurname { get; set; }

        public int AccountId { get; set; }

        [DisplayName("Номер карты")]
        public string Number { get; set; } = string.Empty;

        public string CVC { get; set; } = string.Empty;

        [DisplayName("Период действия")]
        public DateTime Period { get; set; } = DateTime.Now;
    }
}
