using FactoryImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryImplement
{
	public class BankYouBancruptDatabase : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				//optionsBuilder.UseSqlServer(@"Data Source=SHADOWIK\SHADOWIK;Initial Catalog=Factory;Integrated Security=True;TrustServerCertificate=True");
				optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-CFLH20EE\SQLEXPRESS;Initial Catalog=Factory;Integrated Security=True;MultipleActiveResultSets=True;;TrustServerCertificate=True");
			}
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Account> Accounts { set; get; }

        public virtual DbSet<Card> Cards { set; get; }

        public virtual DbSet<Cashier> Cashiers { set; get; }

        public virtual DbSet<CashWithdrawal> CashWithdrawals { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

        public virtual DbSet<Debiting> Debitings { set; get; }

        public virtual DbSet<Crediting> Creditings { set; get; }

        public virtual DbSet<MoneyTransfer> MoneyTransfers { set; get; }
    }
}
