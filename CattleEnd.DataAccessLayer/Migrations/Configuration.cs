namespace CattleEnd.DataAccessLayer.Migrations
{
    using Models;
    using SharedModels.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CattleEnd.DataAccessLayer.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "\\CattleEnd.DataAccessLayer\\Data\\");
        }

        protected override void Seed(DatabaseContext context)
        {
            //context.WarriorSpecies.Add(new WarriorSpecies
            //{
            //    Name = "Man"
            //});
            //context.WarriorSpecies.Add(new WarriorSpecies
            //{
            //    Name = "Hobbit"
            //});
            //context.WarriorSpecies.Add(new WarriorSpecies
            //{
            //    Name = "Elf"
            //});
            //context.WarriorSpecies.Add(new WarriorSpecies
            //{
            //    Name = "Dwarf"
            //});
            //context.WarriorSpecies.Add(new WarriorSpecies
            //{
            //    Name = "Wizard"
            //});
            //context.SaveChanges();
        }
    }
}
