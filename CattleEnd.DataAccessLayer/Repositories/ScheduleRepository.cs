using CattleEnd.DataAccessLayer.Models;
using CattleEnd.SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattleEnd.DataAccessLayer.Repositories
{
    public class ScheduleRepository
    {
        public int DaysToSchedule { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["DaysToSchedule"]);

        public List<Schedule> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                var schedules = context.Schedules
                    .Include("Warrior")
                    .OrderBy(w => w.GuardDate)
                    .ToList();
                return schedules;
            }
        }

        public DateTime GetLastActiveDate()
        {
            using (var context = new DatabaseContext())
            {
                var dates = context.Schedules
                    .Where(d => d.GuardDate > DateTime.Now)
                    .OrderBy(d => d.GuardDate)
                    .Select(d => d.GuardDate)
                    .ToList();

                for (var i = 1; i <= DaysToSchedule; i++)
                {
                    var freeDate = DateTime.Now.Date.AddDays(i);
                    if (!dates.Contains(freeDate))
                    {
                        return freeDate.Date.AddDays(-1);
                    }
                }
                return default(DateTime);
            }
        }

        public DateTime GetNextGuardDate(Warrior warrior)
        {
            using (var context = new DatabaseContext())
            {
                var guardDate = context.Schedules
                    .OrderBy(s => s.GuardDate)
                    .Where(s => s.GuardDate > DateTime.Now)
                    .Where(s => s.WarriorId == warrior.Id)
                    .Select(s => s.GuardDate)
                    .FirstOrDefault();

                return guardDate;
            }
        }

        public void ArrangeSchedule(Warrior affectedWarrior)
        {
            using (var context = new DatabaseContext())
            {
                var warriors = context.Warriors
                    .Where(w => w.Deleted == false)
                    .OrderBy(w => w.Name)
                    .ToList();

                //If there is no active dates in following x days 
                //then get next guard date for previous warrior in list
                var initialDate = GetLastActiveDate();
                if (initialDate == default(DateTime))
                {
                    Warrior previousWarrior = null;
                    var elementIndex = warriors.FindIndex(w => w.Name == affectedWarrior.Name);
                    if (warriors.Count > 1)
                    {
                        if (elementIndex == 0)
                        {
                            previousWarrior = warriors.ElementAt(warriors.Count - 1);
                        }
                        else
                        {
                            previousWarrior = warriors.ElementAt(elementIndex - 1);
                        }
                    }
                    else
                    {
                        previousWarrior = warriors.ElementAt(elementIndex);
                    }
                    initialDate = GetNextGuardDate(previousWarrior);
                }

                if (initialDate != default(DateTime))
                {
                    var schedules = context.Schedules
                        .OrderBy(s => s.GuardDate)
                        .Where(s => s.GuardDate >= initialDate)
                        .ToList();

                    var lastActiveSchedule = schedules
                        .Where(s => s.GuardDate.Date == initialDate.Date)
                        .SingleOrDefault();

                    var lastActiveWarrior = lastActiveSchedule != null ? lastActiveSchedule.Warrior : null;

                    var listIndex = 0;
                    if (lastActiveWarrior != null)
                    {
                        listIndex = warriors.FindIndex(w => w.Name == lastActiveWarrior.Name) + 1;
                    }

                    var totalDays = DaysToSchedule - (initialDate.Date - DateTime.Now.Date).Days;
                    for (var days = 1; days <= totalDays; days++)
                    {
                        var date = initialDate.Date.AddDays(days);
                        var selectedWarrior = warriors.ElementAt(listIndex);
                        var selectedSchedule = schedules
                            .Where(s => s.GuardDate.Date == date.Date)
                            .SingleOrDefault();

                        if (selectedSchedule == null)
                        {
                            var newSchedule = new Schedule()
                            {
                                WarriorId = selectedWarrior.Id,
                                GuardDate = date.Date
                            };
                            context.Schedules.Add(newSchedule);
                        }
                        else
                        {
                            selectedSchedule.WarriorId = selectedWarrior.Id;
                            context.Entry(selectedSchedule).State = EntityState.Modified;
                        }
                        listIndex = (listIndex == warriors.Count - 1) ? 0 : listIndex++;
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}


