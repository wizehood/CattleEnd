using CattleEnd.DataAccessLayer.Models;
using CattleEnd.SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattleEnd.DataAccessLayer.Repositories
{
    public class WarriorRepository
    {
        public List<Warrior> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                var warriors = context.Warriors
                    .Include("Species")
                    .Where(w => w.Deleted == false)
                    .OrderBy(w => w.Name)
                    .ToList();

                return warriors;
            }
        }

        public Warrior GetById(int id)
        {
            using (var context = new DatabaseContext())
            {
                var warrior = context.Warriors
                    .Include("Species")
                    .Where(w => w.Id == id)
                    .Single();

                return warrior;
            }
        }

        public List<WarriorSpecies> GetSpecies()
        {
            using (var context = new DatabaseContext())
            {
                var species = context.WarriorSpecies.ToList();
                return species;
            }
        }

        public List<string> GetEmails()
        {
            using (var context = new DatabaseContext())
            {
                var emails = context.Warriors
                    .Where(w => w.Deleted == false)
                    .Select(w => w.Email)
                    .ToList();

                return emails;
            }
        }

        public int Create(Warrior warrior)
        {
            using (var context = new DatabaseContext())
            {
                context.Warriors.Add(warrior);
                context.SaveChanges();
                return warrior.Id;
            }
        }

        public void Delete(int id)
        {
            using (var context = new DatabaseContext())
            {
                var warrior = context.Warriors.Find(id);
                warrior.Deleted = true;
                context.SaveChanges();
            }
        }
    }
}
