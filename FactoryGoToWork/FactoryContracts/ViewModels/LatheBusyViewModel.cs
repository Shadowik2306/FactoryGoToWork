using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class LatheBusyViewModel : ILatheBusyModel
    {
        public int Id { get; set; }

        [DisplayName("Процент занятости")]
        public int Percent { get; set; }
        [DisplayName("Дата занятости")]
        public DateTime Date { get; set; }
    }
}
