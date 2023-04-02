using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class Card : ICardModel
    {
        public int Id { get; set; }

        [Required]
        public int ClientID { get; set; }

        public virtual Client Client { get; set; } = new();

        [Required]
        public int AccountId { get; set; }

        [Required]
        public string Number { get; set; } = String.Empty;

        [Required]
        public string CVC { get; set; }

        [Required]
        public DateTime Period { get; set; } = DateTime.Now;

        [ForeignKey("CardId")]
        public virtual List<Crediting> Creditings { get; set; } = new();

        [ForeignKey("CardId")]
        public virtual List<Debiting> Debitings { get; set; } = new();

        public CardViewModel GetViewModel => new()
        {
            Id = Id,
            ClientID = ClientID,
            ClientSurname = Client.Surname,
            Number = Number,
            Period = Period,
            CVC = CVC
        };

        public static Card Create(BankYouBancruptDatabase context, CardBindingModel model)
        {
            return new Card()
            {
                Id = model.Id,
                ClientID = model.ClientID,
                Client = context.Clients.First(x => x.Id == model.ClientID),
                Number = model.Number,
                Period = model.Period,
                CVC = model.CVC
            };
        }

        public void Update(CardBindingModel model)
        {
            Period = model.Period;
        }
    }
}

