using CattleEnd.SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CattleEnd.DataAccessLayer.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DatabaseContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Warrior> Warriors { get; set; }

        public DbSet<WarriorSpecies> WarriorSpecies { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
    }
}
