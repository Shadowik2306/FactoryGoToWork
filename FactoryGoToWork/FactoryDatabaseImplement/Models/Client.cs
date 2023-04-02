using FactoryContracts.BindingModels;
using FactoryContracts.ViewModels;
using FactoryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement.Models
{
    public class Client : IClientModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        public string Patronymic { get; set; } = string.Empty;

        [Required]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("ClientID")]
        public virtual List<Card> Cards { get; set; } = new();

        public ClientViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Patronymic = Patronymic,
            Telephone = Telephone,
            Email = Email,
            Password = Password
        };

        public static Client Create(ClientBindingModel model)
        {
            return new Client()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Telephone = model.Telephone,
                Email = model.Email,
                Password = model.Password
            };
        }

        public void Update(ClientBindingModel model)
        {
            Name = model.Name;
            Surname = model.Surname;
            Patronymic = model.Patronymic;
            Telephone = model.Telephone;
            Email = model.Email;
            Password = model.Password;
        }
    }
}

