﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDataModels.Models
{
    public interface IPlanModel : IId
    {
        string PlanName { get; }
		DateTime StartDate { get; }
		DateTime EndDate { get; }
		Dictionary<int, (IReinforcedModel, int)> PlanReinforcedes { get; }        
    }
}
