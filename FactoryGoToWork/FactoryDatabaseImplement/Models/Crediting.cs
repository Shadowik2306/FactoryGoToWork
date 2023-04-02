using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class Crediting : ICreditingModel
    {

        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        public virtual Card Card { get; set; } = new();

        [Required]
        public int Sum { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;


        public CreditingViewModel GetViewModel => new()
        {
            Id = Id,
            CardId = CardId,
            CardNumber = Card.Number,
            Sum = Sum,
            Date = Date
        };

        public static Crediting Create(BankYouBancruptDatabase context, CreditingBindingModel model)
        {
            return new Crediting()
            {
                Id = model.Id,
                CardId = model.CardId,
                Card = context.Cards.First(x => x.Id == model.CardId),
                Sum = model.Sum,
                Date = model.Date
            };
        }

        public void Update(CreditingBindingModel model)
        {
            Date = model.Date;
        }
    }
}
