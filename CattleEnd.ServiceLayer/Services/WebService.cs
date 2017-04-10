using CattleEnd.DataAccessLayer.Repositories;
using CattleEnd.SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattleEnd.ServiceLayer.Services
{
    public class WebService
    {
        public WarriorRepository WarriorRepository { get; set; }

        public ScheduleRepository ScheduleRepository { get; set; }

        public WebService()
        {
            WarriorRepository = new WarriorRepository();
            ScheduleRepository = new ScheduleRepository();
        }

        public List<Warrior> GetAllWarriors()
        {
            try
            {
                var entities = WarriorRepository.GetAll();
                return entities;
            }
            catch
            {
                return new List<Warrior>();
            }
        }

        public Warrior GetWarriorById(int id)
        {
            try
            {
                var entity = WarriorRepository.GetById(id);
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public List<WarriorSpecies> GetWarriorSpecies()
        {
            try
            {
                var entities = WarriorRepository.GetSpecies();
                return entities;
            }
            catch
            {
                return new List<WarriorSpecies>();
            }
        }

        public List<Schedule> GetAllSchedules()
        {
            try
            {
                var entities = ScheduleRepository.GetAll();
                return entities;
            }
            catch
            {
                return new List<Schedule>();
            }
        }

        public bool CreateWarrior(Warrior warrior)
        {
            try
            {
                WarriorRepository.Create(warrior);
                ScheduleRepository.ArrangeSchedule(warrior);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteWarrior(int id)
        {
            try
            {
                WarriorRepository.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
