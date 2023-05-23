using FactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryDatabaseImplement
{
	public class FactoryDatabase : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				if (System.Environment.MachineName == "SHADOWIK")
				{
					optionsBuilder.UseSqlServer(@"Data Source=SHADOWIK\SHADOWIK;Initial Catalog=Factory;Integrated Security=True;TrustServerCertificate=True");
				}
				else
				{
					optionsBuilder.UseSqlServer(@"Data Source=PREMIXHOME\SQLEXPRESS05;Initial Catalog=Factory;Integrated Security=True;TrustServerCertificate=True");
				}
			}
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Master> Masters { set; get; }
        public virtual DbSet<Engenier> Engeniers { set; get; }
        public virtual DbSet<Reinforced> Reinforceds { set; get; }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Lathe> Lathes { set; get; }
        public virtual DbSet<LatheBusy> LatheBusies { set; get; }
        public virtual DbSet<LatheReinforced> LatheReinforcedes { set; get; }
		public virtual DbSet<LatheComponent> LatheComponents { set; get; }
		public virtual DbSet<Plan> Plans { set; get; }
        public virtual DbSet<Stage> Stages { set; get; }
        public virtual DbSet<PlanReinforced> PlanReinforceds { set; get; }
        public virtual DbSet<ComponentPlans> ComponentPlans { set; get; }

    }
}
