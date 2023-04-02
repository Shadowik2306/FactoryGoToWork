﻿using FactoryContracts.BindingModels;
using FactoryContracts.SearchModels;
using FactoryContracts.StoragesContracts;
using FactoryContracts.ViewModels;
using FactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement.Implements
{
    public class LatheStorage : ILatheStorage
    {
        public List<LatheViewModel> GetFullList()
        {
            using var context = new FactoryDatabase();
            return context.Lathes.Select(x => x.GetViewModel).ToList();
        }

        public List<LatheViewModel> GetFilteredList(LatheSearchModel model)
        {
            if (string.IsNullOrEmpty(model.LatheName))
            {
                return new();
            }
            using var context = new FactoryDatabase();
            return context.Lathes.Where(x => x.LatheName.Contains(model.LatheName)).Select(x => x.GetViewModel).ToList();
        }

        public LatheViewModel? GetElement(LatheSearchModel model)
        {
            if (string.IsNullOrEmpty(model.LatheName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            return context.Lathes.FirstOrDefault(x => 
            (!string.IsNullOrEmpty(model.LatheName) && x.LatheName == model.LatheName) || 
            (model.Id.HasValue && x.Id == model.Id))
                ?.GetViewModel;
        }

        public LatheViewModel? Insert(LatheBindingModel model)
        {
            var newLathe = Lathe.Create(new FactoryDatabase(), model);
            if (newLathe == null)
            {
                return null;
            }
            using var context = new FactoryDatabase();
            context.Lathes.Add(newLathe);
            context.SaveChanges();
            return newLathe.GetViewModel;
        }

        public LatheViewModel? Update(LatheBindingModel model)
        {
            using var context = new FactoryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var Lathe = context.Lathes.FirstOrDefault(rec => rec.Id == model.Id);
                if (Lathe == null)
                {
                    return null;
                }
                Lathe.Update(model);
                context.SaveChanges();
                Lathe.UpdateComponents(context, model);
                transaction.Commit();
                return Lathe.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public LatheViewModel? Delete(LatheBindingModel model)
        {
            using var context = new FactoryDatabase();
            var element = context.Lathes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Lathes.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
