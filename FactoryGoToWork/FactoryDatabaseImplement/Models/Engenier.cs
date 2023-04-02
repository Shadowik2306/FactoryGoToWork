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
    public class Engenier : IEngenierModel
    {
        public int Id { get; set; }

        [Required]
        public string Fio { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;


        public EngenierViewModel GetViewModel => new()
        {
            Id = Id,
            Fio = Fio,
            Email = Email,
            Password = Password
        };

        public static Engenier Create(EngenierBindingModel model)
        {
            return new Engenier()
            {
                Id = model.Id,
                Fio = model.Fio,
                Email = model.Email,
                Password = model.Password
            };
        }

        public void Update(EngenierBindingModel model)
        {
            Fio = model.Fio;
            Email = model.Email;
            Password = model.Password;
        }
    }
}

