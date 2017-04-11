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
                    .Where(s => s.GuardDate > DateTime.Now)
                    .Where(s => s.WarriorId == warrior.Id)
                    .OrderBy(s => s.GuardDate)
                    .Select(s => s.GuardDate)
                    .FirstOrDefault();

                return guardDate;
            }
        }

        public void ArrangeSchedule(Warrior affectedWarrior, List<Warrior> warriors)
        {
            using (var context = new DatabaseContext())
            {
                var index = warriors.FindIndex(w => w.Id == affectedWarrior.Id);

                //If there is no active dates in following x days 
                //then get next guard date for previous warrior in list
                var initialDate = GetLastActiveDate();
                if (initialDate == default(DateTime))
                {
                    Warrior previousWarrior = null;
                    if (warriors.Count > 1)
                    {
                        if (index == 0)
                        {
                            previousWarrior = warriors.ElementAt(warriors.Count - 1);
                        }
                        else
                        {
                            previousWarrior = warriors.ElementAt(index - 1);
                        }
                    }
                    else
                    {
                        previousWarrior = warriors.ElementAt(index);
                    }
                    initialDate = GetNextGuardDate(previousWarrior);
                }

                if (initialDate != default(DateTime))
                {
                    var schedules = context.Schedules
                        .Where(s => s.GuardDate > DateTime.Now)
                        .OrderBy(s => s.GuardDate)
                        .ToList();

                    //Initial schedule state
                    if (schedules.Count == 0)
                    {
                        index = 0;
                    }

                    //Marginal cases for date and index when warrior is deleted
                    if (affectedWarrior.Deleted == true)
                    {
                        warriors.Remove(affectedWarrior);

                        var firstSchedule = schedules.FirstOrDefault();
                        if (firstSchedule != null && firstSchedule.WarriorId == affectedWarrior.Id)
                        {
                            initialDate = firstSchedule.GuardDate.Date.AddDays(-1);
                        }
                        if (index == warriors.Count)
                        {
                            index = 0;
                        }
                    }

                    var totalDays = DaysToSchedule - (initialDate.Date - DateTime.Now.Date).Days;
                    for (var days = 1; days <= totalDays; days++)
                    {
                        var date = initialDate.Date.AddDays(days);
                        var selectedWarrior = warriors.ElementAt(index);
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
                        index = (index == warriors.Count - 1) ? 0 : index + 1;
                    }
                    context.SaveChanges();
                }
            }
        }

        public void AssignAdditionalDay(Warrior warrior)
        {
            using (var context = new DatabaseContext())
            {
                var guardDate = GetNextGuardDate(warrior);
                if (guardDate == default(DateTime))
                {
                    guardDate = DateTime.Now.Date;
                }

                var schedules = context.Schedules
                    .Where(s => s.GuardDate > guardDate)
                    .OrderBy(s => s.GuardDate)
                    .ToList();

                //Copy list with neccessary info
                var tempList = schedules
                    .Select(s => new Schedule { WarriorId = s.WarriorId })
                    .ToList();

                if (schedules.Count > 0)
                {
                    schedules[0].WarriorId = warrior.Id;
                    context.Entry(schedules[0]).State = EntityState.Modified;

                    for (int i = 1; i < schedules.Count; i++)
                    {
                        schedules[i].WarriorId = tempList[i - 1].WarriorId;
                        context.Entry(schedules[i]).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
        }

        public string GetResponsibleWarriorName()
        {
            using (var context = new DatabaseContext())
            {
                var schedules = context.Schedules
                    .Include("Warrior")
                    .ToList();

                var matchedSchedule = schedules
                    .Where(s => s.GuardDate.Date == DateTime.Now.Date)
                    .SingleOrDefault();

                if (matchedSchedule != null)
                {
                    return matchedSchedule.Warrior.Name;
                }
                return string.Empty;
            }
        }

        public void ClearSchedule()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Schedules");
            }
        }
    }
}


