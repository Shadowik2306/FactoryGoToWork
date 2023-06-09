﻿using FactoryDataModels.Models;
using System.ComponentModel;

namespace FactoryContracts.ViewModels
{
    public class ComponentViewModel : IComponentModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string ComponentName { get; set; } = string.Empty;

        [DisplayName("Цена")]
        public double Cost { get; set; }

		public int EngenierId { get; set; }

        public Dictionary<int, (IPlanModel, int)> ComponentPlans { get; set; } = new();
	}
}
